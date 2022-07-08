using System.ComponentModel.DataAnnotations;

namespace CryptoWalletView.Api.Data.Entities;

public class CoinEntity
{
    [Key]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    public string ImageSource { get; set; }
    public bool IsTrading { get; set; }
    
    public virtual ICollection<PairEntity> PairsAsBase { get; set; }
    // public virtual ICollection<PairEntity> PairsAsQuote { get; set; }
}