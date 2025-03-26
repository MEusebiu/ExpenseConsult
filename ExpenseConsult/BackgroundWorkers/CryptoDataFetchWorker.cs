using ExpenseWebAPI.BackgroundWorkers.Models;
using System.Text.Json;

namespace ExpenseWebAPI.BackgroundWorkers;

public class CryptoDataFetchWorker : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CryptoDataFetchWorker> _logger;
    private const string cryptoApiUrl = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum,doge&vs_currencies=usd";

    public CryptoDataFetchWorker(ILogger<CryptoDataFetchWorker> logger)
    {
        _httpClient = new HttpClient();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Fetching crypto exchange rates...");

            var response = await _httpClient.GetAsync(
                cryptoApiUrl,
                stoppingToken
            );

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var cryptoPrices = JsonSerializer.Deserialize<CryptoResponse>(data, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (cryptoPrices != null)
                {
                    _logger.LogInformation($"Exchange Rates at {DateTime.UtcNow}");
                    _logger.LogInformation($"Bitcoin: $ {cryptoPrices.Bitcoin.USD}, Ethereum: $ {cryptoPrices.Ethereum.USD}, Dogecoin: $ {cryptoPrices.Doge.USD}");
                }
            }
            else
            {
                _logger.LogError("Failed to fetch currency exchange rates.");
            }

            await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
        }
    }
}
