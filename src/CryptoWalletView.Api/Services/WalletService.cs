using Binance.Net.Clients;
using CryptoWalletView.Api.Interfaces;
using CryptoWalletView.Api.Models;

namespace CryptoWalletView.Api.Services;

public class WalletService : IWalletService
{
    private readonly SymbolService _symbolService;
    private readonly MarketDataService _marketDataService;
    private readonly BinanceClient _binanceClient;

    public WalletService(SymbolService symbolService, MarketDataService marketDataService, BinanceClient binanceClient)
    {
        _symbolService = symbolService;
        _marketDataService = marketDataService;
        _binanceClient = binanceClient;
    }

    public async Task<WalletInfo> GetWalletinfo()
    {
        var wallet = new WalletInfo();
        var response = await _binanceClient.SpotApi.Account.GetAccountInfoAsync();
        if(!response.Success) throw new Exception(response.Error.Message);

        var assetsCodes = response.Data.Balances.Select(b => b.Asset);
        var symbols = await _symbolService.GetForList(assetsCodes);
        var candles = await _marketDataService.GetCandlesForToday(assetsCodes);

        foreach(var asset in response.Data.Balances)
        {
            var code = asset.Asset;
            var walletItem = new WalletAssetInfo();
            walletItem.Symbol = symbols.FirstOrDefault(s => s.Code == code);
            walletItem.Amount = asset.Total;
            walletItem.Price = candles[code].Close;
            walletItem.ChangeValue = candles[code].Close - candles[code].Open;
            walletItem.ChangePercent = walletItem.ChangeValue / candles[code].Open;

            wallet.Assets.Add(walletItem);
        }
        return wallet;
    }
}