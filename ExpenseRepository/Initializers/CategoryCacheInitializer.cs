using ExpenseDataAccessLayer.Models;
using ExpenseDataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExpenseDataAccessLayer.Initializers
{
    public class CategoryCacheInitializer : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CategoryCacheInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var categoryRepository = scope.ServiceProvider.GetRequiredService<IRepository<string, Category>>();
                await categoryRepository.LoadCacheAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
