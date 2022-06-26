using Domain.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
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

            builder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> ?Posts { get; set; }
        public DbSet<Comment> ?Comments { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //TODO: Swap builders on dev 
            optionsBuilder.UseNpgsql("Username=postgres;Password=password;Server=postgres;Port=5432;Database=BlogApp;Integrated Security=true;Pooling=true");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BlogApp;Username=postgres;Password=password");
        }
    }
}