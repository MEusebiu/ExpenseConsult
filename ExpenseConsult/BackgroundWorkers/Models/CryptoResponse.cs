namespace ExpenseWebAPI.BackgroundWorkers.Models;

public class CryptoResponse
{
    public CryptoPrice Bitcoin { get; set; }
    public CryptoPrice Ethereum { get; set; }
    public CryptoPrice Doge { get; set; }
}