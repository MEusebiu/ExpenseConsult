//using ExpenseConsult.Models;
//using ExpenseConsult.Repositories;

//namespace ExpenseConsult.Initializers
//{
//    public class UserCacheInitializer : IHostedService
//    {
//        private readonly IServiceScopeFactory _scopeFactory;

//        public UserCacheInitializer(IServiceScopeFactory scopeFactory)
//        {
//            _scopeFactory = scopeFactory;
//        }

//        public async Task StartAsync(CancellationToken cancellationToken)
//        {
//            using (var scope = _scopeFactory.CreateScope())
//            {
//                var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<string, User>>();
//                await userRepository.LoadCacheAsync();
//            }
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            return Task.CompletedTask;
//        }
//    }
//}
