
using CryptoWalletView.Api.Models;

namespace CryptoWalletView.Api.Interfaces;
public interface ICoinInfoService
{
    Task<IEnumerable<CoinInfo>> GetCoinInfoList(IEnumerable<string> codes);
}