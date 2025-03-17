
using ExpenseConsult.Models;
using ExpenseConsult.Repositories;

namespace ExpenseConsult
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
                var expenseRepository = scope.ServiceProvider.GetRequiredService<IRepository<int, Expense>>();
                await expenseRepository.LoadCacheAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
