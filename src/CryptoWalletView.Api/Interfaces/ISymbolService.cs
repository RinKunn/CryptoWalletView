
using CryptoWalletView.Api.Models;

namespace CryptoWalletView.Api.Interfaces;
public interface ISymbolSservice
{
    Task<Symbol> GetSymbol(string code);
    Task<IEnumerable<Symbol>> GetForList(IEnumerable<string> coinCodes);
}