namespace CryptoWalletView.Api.Models;

public class WalletInfo
{
    public decimal Total { get; set; }
    public decimal ChangeValue { get; set; }
    public decimal ChangePercent { get; set; }
    public List<WalletAssetInfo> Assets { get; set; } = new List<WalletAssetInfo>();
}