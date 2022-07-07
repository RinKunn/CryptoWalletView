namespace CryptoWalletView.Api.ViewModels;
public class WalletVM
{  
    public List<WalletItemVM> Data { get; set; }

    public class WalletItemVM
    {
        public SymbolVM Symbol { get; set; }
        public decimal Amount { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal ChangeValue { get; set; }
        public decimal ChangePercent { get; set; }
    }

    public class SymbolVM
    {
        public string ImageSource { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}