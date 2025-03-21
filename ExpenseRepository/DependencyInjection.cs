using DataAccessLayer;
using ExpenseDataAccessLayer.Initializers;
using ExpenseDataAccessLayer.Models;
using ExpenseDataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ExpenseDataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), schema => schema.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddHostedService<ExpenseCacheInitializer>();
            services.AddHostedService<CategoryCacheInitializer>();

            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }
    }
}
