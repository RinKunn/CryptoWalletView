using Binance.Net.Interfaces.Clients;
using CryptoWalletView.Api.Data;
using CryptoWalletView.Api.Data.Entities;
using CryptoWalletView.Api.Interfaces;
using CryptoWalletView.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWalletView.Api.Services;

public class CoinInfoService : ICoinInfoService
{
    private readonly CryptoContext _context;
    private readonly IBinanceClient _client;

    public CoinInfoService(CryptoContext context, IBinanceClient client)
    {
        _context = context;
        _client = client;
    }

    public async Task<IEnumerable<CoinInfo>> GetCoinInfoList(IEnumerable<string> coinCodes)
    {
        if(!_context.Coins.Any())
            await Seed();
        
        var entities = await _context.Coins.Where(s => coinCodes.Contains(s.Code)).ToListAsync();
        return entities.Select(e => Map(e));
    }

    private async Task Seed()
    {
        var coins = await GetCoinsInfoFromBinance();
        var pairs = await GetPairsFromBinance();

        _context.Coins.AddRange(coins);
        _context.Pairs.AddRange(pairs);
        await _context.SaveChangesAsync();
    }

    private async Task<IEnumerable<CoinEntity>> GetCoinsInfoFromBinance()
    {
        var response = await _client.SpotApi.Account.GetUserAssetsAsync();
        if(!response.Success)
            throw new Exception($"Cannot load assets: {response.Error.Message}");
        
        var coins = response.Data.Select(d => new CoinEntity
        {
            Code = d.Asset,
            Name = d.Name,
            IsTrading = d.Trading,
            ImageSource = $"https://cryptoicons.org/api/color/{d.Asset.ToLower()}/50",
        });
        return coins;
    }

    private async Task<IEnumerable<PairEntity>> GetPairsFromBinance()
    {
        var response = await _client.SpotApi.ExchangeData.GetMarginSymbolsAsync();
        if(!response.Success) 
            throw new Exception($"Cannot load pairs: {response.Error.Message}");

        var pairs = response.Data.Select(d => new PairEntity
        {
            BaseCode = d.BaseAsset,
            QuoteCode = d.QuoteAsset,
            BinanceId = d.Id,
            Symbol = d.Symbol,
        });
        return pairs;
    }

    private CoinInfo Map(CoinEntity entity) =>
        new CoinInfo
        {
            Code = entity.Code,
            Name = entity.Name,
            ImageSource = entity.ImageSource,
            IsTrading = entity.IsTrading,
        };
}