using CryptoWalletView.Api.Models;

namespace CryptoWalletView.Api.Interfaces;
public interface IMarketDataService
{
    Task<Dictionary<string, decimal>> GetPrices(params string[] symbols);
    Task<Dictionary<string, Candle>> GetCandlesForToday(IEnumerable<string> symbols);
}