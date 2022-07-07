using Binance.Net.Interfaces.Clients;
using CryptoWalletView.Api.Data;
using CryptoWalletView.Api.Data.Entities;
using CryptoWalletView.Api.Interfaces;
using CryptoWalletView.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWalletView.Api.Services;

public class SymbolService : ISymbolSservice
{
    private readonly CryptoContext _context;
    private readonly IBinanceClient _client;

    public SymbolService(CryptoContext context, IBinanceClient client)
    {
        _context = context;
        _client = client;
    }

    public async Task<IEnumerable<Symbol>> GetForList(IEnumerable<string> coinCodes)
    {
        if(!_context.Symbols.Any())
            await BringDataToContext();
        
        var entities = await _context.Symbols.Where(s => coinCodes.Contains(s.Code)).ToListAsync();
        return entities.Select(e => Map(e));
    }

    public async Task<Symbol> GetSymbol(string coinCode)
    {
        if(!_context.Symbols.Any())
            await BringDataToContext();

        var entity = await _context.Symbols.FirstOrDefaultAsync(s => s.Code == coinCode);
        return Map(entity);
    }

    private async Task BringDataToContext()
    {
        var response = await _client.SpotApi.Account.GetUserAssetsAsync();
        if(!response.Success) throw new Exception(response.Error.Message);

        var symbols = response.Data.Select(d =>
            new SymbolEntity
            {
                Code = d.Asset,
                Name = d.Name,
                IsTrading = d.Trading,
                ImageSource = $"https://cryptoicons.org/api/color/{d.Asset.ToLower()}/50",
            });
            
        _context.Symbols.AddRange(symbols);
        await _context.SaveChangesAsync();
    }

    private Symbol Map(SymbolEntity entity) =>
        new Symbol
        {
            Code = entity.Code,
            Name = entity.Name,
            ImageSource = entity.ImageSource,
        };
}