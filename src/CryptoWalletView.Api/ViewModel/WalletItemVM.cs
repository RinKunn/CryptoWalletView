namespace CryptoWalletView.Api.ViewModels;
public class WalletItemVM
{
    public SymbolVM Symbol { get; set; }
    public decimal Amount { get; set; }
    public decimal AvgPrice { get; set; }
    public decimal ChangeValue { get; set; }
    public decimal ChangePercent { get; set; }
}