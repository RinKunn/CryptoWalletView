namespace CryptoWalletView.Api.Models;

public class WalletAssetInfo
{
    public CoinInfo CoinInfo { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public decimal ChangeValue { get; set; }
    public decimal ChangePercent { get; set; }
}