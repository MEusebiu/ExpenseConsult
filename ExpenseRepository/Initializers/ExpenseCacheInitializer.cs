using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;
using ExpenseDataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExpenseDataAccessLayer.Initializers
{
    public class ExpenseCacheInitializer : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ExpenseCacheInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var expenseRepository = scope.ServiceProvider.GetRequiredService<IRepository<string, Expense>>();
                await expenseRepository.LoadCacheAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
