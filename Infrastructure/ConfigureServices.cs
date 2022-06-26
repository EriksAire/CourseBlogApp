using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Repository;
using Domain.Models;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");


            //TODO: Get rid of useless comments
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(connectionString));
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DockerCommandsConnectionString")));

            //TODO: Get rid of 
            using (var context = services.BuildServiceProvider().GetService<ApplicationDbContext>())
            {
                context.Database.Migrate();
            }

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddRoles<IdentityRole>()
            // .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentityServer()
            //.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthorization();
            services.AddAuthentication();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<Post>, Repository<Post>>();
            services.AddScoped<IPostService,PostService>();

            return services;
        }
    }
}
