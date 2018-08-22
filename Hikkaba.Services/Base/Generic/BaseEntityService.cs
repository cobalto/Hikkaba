﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Hikkaba.Data.Context;
using Hikkaba.Data.Entities.Base.Generic;
using Hikkaba.Infrastructure.Exceptions;
using Hikkaba.Models.Dto;
using Hikkaba.Models.Dto.Base.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hikkaba.Services.Base.Generic
{
    public interface IBaseEntityService<TDto, TEntity, TPrimaryKey>
    {
        Task<TDto> GetAsync(TPrimaryKey id);
        Task<IList<TDto>> ListAsync(Expression<Func<TEntity, bool>> where = null);
        Task<IList<TDto>> ListAsync<TOrderKey>(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, TOrderKey>> orderBy = null, bool isDescending = false);
        Task<BasePagedList<TDto>> PagedListAsync<TOrderKey>(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, TOrderKey>> orderBy = null, bool isDescending = false, PageDto page = null);
    }

    public abstract class BaseEntityService<TDto, TEntity, TPrimaryKey> : IBaseEntityService<TDto, TEntity, TPrimaryKey>
        where TDto : class, IBaseDto<TPrimaryKey>
        where TEntity : class, IBaseEntity<TPrimaryKey>
    {
        protected readonly IMapper Mapper;
        protected readonly ApplicationDbContext Context;

        protected abstract DbSet<TEntity> GetDbSet(ApplicationDbContext context);
        
        protected BaseEntityService(IMapper mapper, ApplicationDbContext context)
        {
            Mapper = mapper;
            Context = context;
        }
        
        #region MapMethods
        protected TDto MapEntityToDto(TEntity entity)
        {
            var dto = Mapper.Map<TDto>(entity);
            return dto;
        }

        protected IList<TDto> MapEntityListToDtoList(IList<TEntity> entityList)
        {
            var dtoList = Mapper.Map<List<TDto>>(entityList);
            return dtoList;
        }

        protected TEntity MapDtoToNewEntity(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);
            entity.Id = entity.GenerateNewId();
            return entity;
        }

        protected TEntity MapDtoToExistingEntity(TDto dto, TEntity entity)
        {
            var resultEntity = Mapper.Map(dto, entity);
            return resultEntity;
        }
        #endregion
        
        private async Task<TEntity> GetEntityByIdAsync(TPrimaryKey id)
        {
            var resultEntity = await GetDbSet(Context).FirstOrDefaultAsync(entity => entity.Id.Equals(id));
            if (resultEntity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"{typeof(TEntity)} {id} not found.");
            }
            else
            {
                return resultEntity;
            }
        }

        private async Task<TEntity> GetEntityByExpressionAsync(Expression<Func<TEntity, bool>> where)
        {
            var resultEntity = await GetDbSet(Context).FirstOrDefaultAsync(where);
            if (resultEntity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"{typeof(TEntity)} not found.");
            }
            else
            {
                return resultEntity;
            }
        }

        private IQueryable<TEntity> Query<TOrderKey>(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, TOrderKey>> orderBy = null, bool isDescending = false)
        {
            var query = GetDbSet(Context).AsQueryable();

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                if (isDescending)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }

            return query;
        }

        public async Task<TDto> GetAsync(TPrimaryKey id)
        {
            var entity = await GetEntityByIdAsync(id);
            return MapEntityToDto(entity);
        }

        public async Task<TDto> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            var entity = await GetEntityByExpressionAsync(where);
            return MapEntityToDto(entity);
        }

        public async Task<IList<TDto>> ListAsync(Expression<Func<TEntity, bool>> where = null)
        {
            return await ListAsync<bool>(where);
        }

        public async Task<IList<TDto>> ListAsync<TOrderKey>(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, TOrderKey>> orderBy = null, bool isDescending = false)
        {
            var query = Query(where, orderBy, isDescending);
            var entityList = await query.ToListAsync();
            var dtoList = MapEntityListToDtoList(entityList);
            return dtoList;
        }

        public async Task<BasePagedList<TDto>> PagedListAsync<TOrderKey>(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, TOrderKey>> orderBy = null, bool isDescending = false, PageDto page = null)
        {
            page = page ?? new PageDto();

            var query = Query(where, orderBy, isDescending);

            var pageQuery = query.Skip(page.Skip).Take(page.PageSize);

            var entityList = await pageQuery.ToListAsync();
            var dtoList = MapEntityListToDtoList(entityList);
            var pagedList = new BasePagedList<TDto>
            {
                TotalItemsCount = query.Count(),
                CurrentPage = page,
                CurrentPageItems = dtoList,
            };
            return pagedList;
        }

        protected async Task<TPrimaryKey> CreateAsync(TDto dto, Action<TEntity> setProperties, Action<TEntity> setForeignKeys)
        {
            if (dto == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"{nameof(dto)} is null.");
            }

            var entity = MapDtoToNewEntity(dto);
            setForeignKeys?.Invoke(entity);
            setProperties?.Invoke(entity);

            var entityEntry = await GetDbSet(Context).AddAsync(entity);
            await Context.SaveChangesAsync();

            return entityEntry.Entity.Id;
        }

        protected async Task EditAsync(TDto dto, Action<TEntity> setProperties, Action<TEntity> setForeignKeys)
        {
            if (dto == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"{nameof(dto)} is null.");
            }
            else if (dto.Id.Equals(default(TPrimaryKey)))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"{nameof(dto)}.{nameof(dto.Id)} is default or empty.");
            }

            var entity = await GetEntityByIdAsync(dto.Id);
            MapDtoToExistingEntity(dto, entity);
            setForeignKeys?.Invoke(entity);
            setProperties?.Invoke(entity);

            await Context.SaveChangesAsync();
        }

        protected async Task DeleteAsync(TPrimaryKey id, Action<TEntity> setProperties, Action<TEntity> setForeignKeys)
        {
            if (id.Equals(default(TPrimaryKey)))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"{nameof(id)} is default or empty.");
            }

            var entity = await GetEntityByIdAsync(id);
            setForeignKeys?.Invoke(entity);
            setProperties?.Invoke(entity);
            
            await Context.SaveChangesAsync();
        }
    }
}