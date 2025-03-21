using ExpenseServices.Services.Interfaces;
using ExpenseServices.Services;
using Microsoft.Extensions.DependencyInjection;
using ExpenseDataAccessLayer.Models;
using System.Collections.Concurrent;

namespace ExpenseServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton(new ConcurrentDictionary<string, Expense>());
            services.AddSingleton(new ConcurrentDictionary<string, Category>());

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IExpenseService, ExpenseService>();

            return services;
        }
    }
}
