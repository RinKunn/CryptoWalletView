using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using CryptoWalletView.Api.Interfaces;
using CryptoWalletView.Api.Models;

namespace CryptoWalletView.Api.Services;

public class MarketDataService : IMarketDataService
{
    private readonly IBinanceClient _client;
    private readonly ICoinInfoService _coinInfoService;

    public MarketDataService(IBinanceClient client, ICoinInfoService coinInfoServices)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _coinInfoService = coinInfoServices ?? throw new ArgumentNullException(nameof(coinInfoServices));
    }

    public async Task<Dictionary<string, Candle>> GetTodayCandlesForSymbols(IEnumerable<string> symbols)
    {
        var dict = new Dictionary<string, Candle>();
        foreach (var symbol in symbols)
        {
            var response = await _client.SpotApi.ExchangeData.GetKlinesAsync(
                symbol, 
                Binance.Net.Enums.KlineInterval.OneDay,
                DateTime.Today,
                limit: 1);

            Candle candle = null;
            if(response.Success) 
                candle = Map(response.Data.First());
            dict.Add(symbol, candle);
        }
        return dict;
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