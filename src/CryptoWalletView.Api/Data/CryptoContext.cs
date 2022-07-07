using Microsoft.EntityFrameworkCore;

namespace CryptoWalletView.Api.Data;
public class CryptoContext : DbContext
{
    public CryptoContext (DbContextOptions<CryptoContext> options)
        : base(options)
    {
    }
}