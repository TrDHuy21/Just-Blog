using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class JustBlogContext : DbContext
    {
        public JustBlogContext(DbContextOptions<JustBlogContext> options) : base(options)
        {
        }


        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostRate> PostRates { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // add configuration
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new PostConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new PostRateConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "Admin"
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Contributor"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Nguyen Van Admin", UserName = "admin", Password = "admin", RoleId = 1, CreatedBy = null },
                new User { Id = 2, FullName = "Nguyen Van Contributor", UserName = "contributor", Password = "contributor", RoleId = 2, CreatedBy = 1 }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Social" , Description = "hello"},
                new Category { Id = 2, CategoryName = "Programming", Description = "hello" }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "First Post",
                    Content = "This is the content of the first post.",
                    Summary = "Summary of the first post",
                    Views = 100,
                    IsPublished = true,
                    CategoryId = 1,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Post
                {
                    Id = 2,
                    Title = "Second Post",
                    Content = "This is the content of the second post.",
                    Summary = "Summary of the second post",
                    Views = 150,
                    IsPublished = true,
                    CategoryId = 2,
                    CreatedBy = 2,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagName = "Introduction",
                    PostId = 1
                },
                new Tag
                {
                    TagName = "Getting Started",
                    PostId = 1
                },
                new Tag
                {
                    TagName = "Advanced",
                    PostId = 2
                },
                new Tag
                {
                    TagName = "Tips and Tricks",
                    PostId = 2
                }
            );

            modelBuilder.Entity<PostRate>().HasData(
                new PostRate
                {
                    Point = 4.5m,
                    PostId = 1,
                    UserId = 1
                },
                new PostRate
                {
                    Point = 3.8m,
                    PostId = 2,
                    UserId = 2
                }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    Content = "Great post!",
                    Date = DateTime.Now,
                    PostId = 1,
                    CreatedBy = 2,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Comment
                {
                    Id = 2,
                    Content = "Very informative.",
                    Date = DateTime.Now,
                    PostId = 2,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            );
        }

    }
}
