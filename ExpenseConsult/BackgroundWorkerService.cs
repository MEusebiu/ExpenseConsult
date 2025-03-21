namespace ExpenseWebApi;

public class BackgroundWorkerService : BackgroundService
{
    private ILogger<BackgroundWorkerService> _logger;

    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger)
    {
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var interval = 5000;
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Background worker is running every {interval} in the background");
            await Task.Delay(interval, stoppingToken);
        }
    }
}
