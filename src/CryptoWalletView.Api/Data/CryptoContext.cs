using CryptoWalletView.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoWalletView.Api.Data;
public class CryptoContext : DbContext
{
    public CryptoContext (DbContextOptions<CryptoContext> options)
        : base(options)
    {

    }
    public DbSet<CoinEntity> Coins { get; set; }
    public DbSet<PairEntity> Pairs { get; set; }
    public DbSet<ApiEntity> Apis { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<PairEntity>()
    //         .HasOne(p => p.Base)
    //         .WithMany(c => c.PairsAsBase)
    //         .HasForeignKey(p => p.BaseCode)
    //         .OnDelete(DeleteBehavior.Restrict);

    //     modelBuilder.Entity<PairEntity>()
    //         .HasOne(p => p.Quote)
    //         .WithMany(c => c.PairsAsQuote)
    //         .HasForeignKey(p => p.QuoteCode)
    //         .OnDelete(DeleteBehavior.SetNull);
    // }

}