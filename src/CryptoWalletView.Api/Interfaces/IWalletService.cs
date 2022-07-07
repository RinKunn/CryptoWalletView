using CryptoWalletView.Api.Models;

namespace CryptoWalletView.Api.Interfaces;
public interface IWalletService
{
    Task<WalletInfo> GetWalletinfo();
}