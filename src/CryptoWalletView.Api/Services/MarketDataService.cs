using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using CryptoWalletView.Api.Interfaces;
using CryptoWalletView.Api.Models;

namespace CryptoWalletView.Api.Services;

public class MarketDataService : IMarketDataService
{
    private readonly IBinanceClient _client;

    public MarketDataService(IBinanceClient client)
    {
        _client = client;
    }

    public async Task<Dictionary<string, decimal>> GetPrices(params string[] symbols)
    {
        var response = await _client.SpotApi.ExchangeData.GetPricesAsync(symbols);
        if(!response.Success) throw new Exception(response.Error.Message);
        return response.Data.ToDictionary(d => d.Symbol, d => d.Price);
    }

    public async Task<Dictionary<string, Candle>> GetCandlesForToday(IEnumerable<string> symbols)
    {
        var candles = new Dictionary<string, Candle>();
        foreach (var symbol in symbols)
        {
            var response = await _client.SpotApi.ExchangeData.GetKlinesAsync(
                symbol, 
                Binance.Net.Enums.KlineInterval.OneDay,
                DateTime.Today);
            if(!response.Success) throw new Exception(response.Error.Message);
            candles.Add(symbol, Map(response.Data.First()));
        }
        return candles;
    }

    private Candle Map(IBinanceKline kline)
    {
        return new Candle
        {
            Open = kline.OpenPrice,
            High = kline.HighPrice,
            Low = kline.LowPrice,
            Close = kline.ClosePrice,
            Volume = kline.Volume,
            OpenTime = kline.OpenTime,
            CloseTime = kline.CloseTime,
        };
    }

}