using Blog.DataLayer.Configurations;
using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using Blog.Domain.Entities.Queries;
using Blog.Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Reflection;

namespace Blog.DataLayer
{
    public class ApplicationDbContext : DbContext
    {

        public static readonly ILoggerFactory _loggerFactory = new NLogLoggerFactory();
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<Custom> Custom { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<UserFullInfo> UserFullInfo { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {



            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();

            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name },
                        Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging()
                .UseSqlServer(config["ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedCategories();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
           
        }

    }
}
