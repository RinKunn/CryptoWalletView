namespace CryptoWalletView.Api.Models;

public class WalletInfo
{
    public List<WalletAssetInfo> Assets { get; set; } = new List<WalletAssetInfo>();
}