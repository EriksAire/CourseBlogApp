using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>()
                .HasKey(k => k.ID);
            builder.Entity<Post>()
                .Property(d => d.ID)
                .ValueGeneratedOnAdd();

            builder.Entity<Comment>()
                .HasKey(k => k.ID);
            builder.Entity<Comment>()
                .Property(d => d.ID)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BlogApp;Username=postgres;Password=password");
        }
    }
}