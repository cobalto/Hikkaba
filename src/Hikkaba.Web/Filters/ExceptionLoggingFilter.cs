﻿using System.Linq;
using Hikkaba.Common.Exceptions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Hikkaba.Web.Filters
{
    public class ExceptionLoggingFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ExceptionLoggingFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionLoggingFilter>();
        }

        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            var actionName = context.ActionDescriptor.DisplayName;
            var isModelValid = context.ModelState.IsValid;
            var modelErrors = context.ModelState.Values
                .SelectMany(modelStateEntry => modelStateEntry.Errors.Select(modelError => modelError.ErrorMessage))
                .Join();
            var displayUrl = context.HttpContext.Request.GetDisplayUrl();
            if (ex is HttpResponseException)
            {
                context.HttpContext.Response.StatusCode = (int) ((ex as HttpResponseException).HttpStatusCode);
            }

            _logger?.LogError($"{ex} | {nameof(actionName)}={actionName} | {nameof(isModelValid)}={isModelValid} | {nameof(modelErrors)}={modelErrors} | {nameof(displayUrl)}={displayUrl} | {nameof(context.HttpContext.Response.StatusCode)}={context.HttpContext.Response.StatusCode}");

            context.ExceptionHandled = false;
            base.OnException(context);
        }
    }
}