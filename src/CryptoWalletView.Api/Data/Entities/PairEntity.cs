using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoWalletView.Api.Data.Entities;

public class PairEntity
{
    public int Id { get; set; }
    public long BinanceId { get; set; }
    public string Symbol { get; set; }
    public string BaseCode { get; set; }
    public string QuoteCode { get; set; }

    [ForeignKey(nameof(BaseCode))]
    public virtual CoinEntity Base { get; set; }

    // [ForeignKey(nameof(QuoteCode))]
    // public virtual CoinEntity Quote { get; set; }
}