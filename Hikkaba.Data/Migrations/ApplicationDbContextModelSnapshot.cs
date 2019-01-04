﻿// <auto-generated />
using System;
using Hikkaba.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hikkaba.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hikkaba.Data.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Audio", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("FileExtension")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Hash")
                        .IsRequired();

                    b.Property<Guid>("PostId");

                    b.Property<long>("Size");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Audio");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Document", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("FileExtension")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Hash")
                        .IsRequired();

                    b.Property<Guid>("PostId");

                    b.Property<long>("Size");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Notice", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("AuthorId");

                    b.Property<Guid>("PostId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Picture", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("FileExtension")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Hash")
                        .IsRequired();

                    b.Property<int>("Height");

                    b.Property<Guid>("PostId");

                    b.Property<long>("Size");

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Video", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("FileExtension")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Hash")
                        .IsRequired();

                    b.Property<Guid>("PostId");

                    b.Property<long>("Size");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Video");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Ban", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid?>("CategoryId");

                    b.Property<DateTime>("Created");

                    b.Property<Guid?>("CreatedById");

                    b.Property<DateTime>("End");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LowerIpAddress")
                        .IsRequired();

                    b.Property<DateTime?>("Modified");

                    b.Property<Guid?>("ModifiedById");

                    b.Property<string>("Reason")
                        .IsRequired();

                    b.Property<Guid?>("RelatedPostId");

                    b.Property<DateTime>("Start");

                    b.Property<string>("UpperIpAddress")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("RelatedPostId");

                    b.ToTable("Bans");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Board", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Category", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<Guid>("BoardId");

                    b.Property<DateTime>("Created");

                    b.Property<Guid?>("CreatedById");

                    b.Property<int>("DefaultBumpLimit");

                    b.Property<bool>("DefaultShowThreadLocalUserHash");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsHidden");

                    b.Property<DateTime?>("Modified");

                    b.Property<Guid?>("ModifiedById");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("Alias")
                        .IsUnique();

                    b.HasIndex("BoardId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.CategoryToModerator", b =>
                {
                    b.Property<Guid>("CategoryId");

                    b.Property<Guid>("ApplicationUserId");

                    b.HasKey("CategoryId", "ApplicationUserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("CategoriesToModerators");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Post", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("Created");

                    b.Property<Guid?>("CreatedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSageEnabled");

                    b.Property<string>("Message")
                        .HasMaxLength(8000);

                    b.Property<DateTime?>("Modified");

                    b.Property<Guid?>("ModifiedById");

                    b.Property<Guid>("ThreadId");

                    b.Property<string>("UserAgent")
                        .IsRequired();

                    b.Property<string>("UserIpAddress")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("ThreadId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Thread", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<int>("BumpLimit");

                    b.Property<Guid>("CategoryId");

                    b.Property<DateTime>("Created");

                    b.Property<Guid?>("CreatedById");

                    b.Property<bool>("IsClosed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPinned");

                    b.Property<DateTime?>("Modified");

                    b.Property<Guid?>("ModifiedById");

                    b.Property<bool>("ShowThreadLocalUserHash");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Audio", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.Post", "Post")
                        .WithMany("Audio")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Document", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.Post", "Post")
                        .WithMany("Documents")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Notice", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "Author")
                        .WithMany("CreatedNotices")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hikkaba.Data.Entities.Post", "Post")
                        .WithMany("Notices")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Picture", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.Post", "Post")
                        .WithMany("Pictures")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Attachments.Video", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.Post", "Post")
                        .WithMany("Video")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Ban", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "CreatedBy")
                        .WithMany("CreatedBans")
                        .HasForeignKey("CreatedById");

                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany("ModifiedBans")
                        .HasForeignKey("ModifiedById");

                    b.HasOne("Hikkaba.Data.Entities.Post", "RelatedPost")
                        .WithMany()
                        .HasForeignKey("RelatedPostId");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Category", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.Board", "Board")
                        .WithMany("Categories")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "CreatedBy")
                        .WithMany("CreatedCategories")
                        .HasForeignKey("CreatedById");

                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany("ModifiedCategories")
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.CategoryToModerator", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany("ModerationCategories")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hikkaba.Data.Entities.Category", "Category")
                        .WithMany("Moderators")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Post", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "CreatedBy")
                        .WithMany("CreatedPosts")
                        .HasForeignKey("CreatedById");

                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany("ModifiedPosts")
                        .HasForeignKey("ModifiedById");

                    b.HasOne("Hikkaba.Data.Entities.Thread", "Thread")
                        .WithMany("Posts")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hikkaba.Data.Entities.Thread", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.Category", "Category")
                        .WithMany("Threads")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Hikkaba.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}