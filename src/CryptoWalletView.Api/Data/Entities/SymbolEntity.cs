namespace CryptoWalletView.Api.Data.Entities;

public class SymbolEntity
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string ImageSource { get; set; }
    public bool IsTrading { get; set; }
}