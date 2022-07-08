using Binance.Net.Interfaces.Clients;
using CryptoWalletView.Api.Interfaces;
using CryptoWalletView.Api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoWalletView.Api.Services;

public class WalletService : IWalletService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<WalletService> _logger;
    private readonly ICoinInfoService _coinInfoService;
    private readonly IMarketDataService _marketDataService;
    private readonly IBinanceClient _binanceClient;

    public WalletService(ICoinInfoService coinInfoService, IMarketDataService marketDataService, 
    IBinanceClient binanceClient, ILogger<WalletService> logger, IMemoryCache memoryCache)
    {
        _coinInfoService = coinInfoService;
        _marketDataService = marketDataService;
        _binanceClient = binanceClient;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task<WalletInfo> GetWalletinfo()
    {
        //TODO: change the logic!! Very time consuming
        
        if (_memoryCache.TryGetValue("wallet", out WalletInfo wallet))
        {
            return wallet;
        }

        wallet = new WalletInfo();

        var response = await _binanceClient.SpotApi.Account.GetAccountInfoAsync();
        if(!response.Success) 
            throw new Exception($"Cannot get account info from Binance! Error: {response.Error.Message}");

        var userBalance = response.Data.Balances.Where(b => b.Total > 0).ToList();
        var coinCodes = userBalance.Select(b => b.Asset);
        var coins = await _coinInfoService.GetCoinInfoList(coinCodes);

        var symbols = coinCodes.Where(a => a != "USDT").Select(s => s + "USDT");
        var candles = await _marketDataService.GetTodayCandlesForSymbols(symbols);

        foreach(var asset in userBalance)
        {
            var code = asset.Asset;
            var walletItem = new WalletAssetInfo
            {
                CoinInfo = coins.FirstOrDefault(s => s.Code == code),
                Amount = asset.Total,
            };

            if(code == "USDT")
            {
                walletItem.Price = 1;
            }
            else
            {
                code += "USDT";
                if(candles[code] != null)
                {
                    walletItem.Price = candles[code].Close;
                    walletItem.ChangeValue = candles[code].Close - candles[code].Open;
                    walletItem.ChangePercent = walletItem.ChangeValue / candles[code].Open;
                }
            }
            
            wallet.Assets.Add(walletItem);
        }

        wallet.Total = wallet.Assets.Sum(a => a.Price * a.Amount);
        wallet.ChangeValue = wallet.Assets.Sum(a => a.ChangeValue);
        wallet.ChangePercent = wallet.Total / (wallet.Total - wallet.ChangeValue) - 1;

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        _memoryCache.Set("wallet", wallet, cacheEntryOptions);

        return wallet;
    }
}