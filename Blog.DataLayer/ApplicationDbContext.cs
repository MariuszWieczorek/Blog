using Blog.DataLayer.Extensions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blog.DataLayer
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();

            optionsBuilder.UseSqlServer(config["ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedCategories();
        }

    }
}
