namespace ExpenseConsult;

public class BackgroundWorkerService : BackgroundService
{
    private ILogger<BackgroundWorkerService> _logger;

    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger)
    {
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Background worker is running in the background");
            await Task.Delay(1000, stoppingToken);
        }
    }
}
