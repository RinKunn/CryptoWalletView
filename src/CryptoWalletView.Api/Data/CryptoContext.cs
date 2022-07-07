using CryptoWalletView.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoWalletView.Api.Data;
public class CryptoContext : DbContext
{
    public CryptoContext (DbContextOptions<CryptoContext> options)
        : base(options)
    {
    }

    public DbSet<SymbolEntity> Symbols { get; set; }
}