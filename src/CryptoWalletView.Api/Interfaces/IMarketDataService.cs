using CryptoWalletView.Api.Models;

namespace CryptoWalletView.Api.Interfaces;
public interface IMarketDataService
{
    Task<Dictionary<string, Candle>> GetTodayCandlesForSymbols(IEnumerable<string> symbols);
}