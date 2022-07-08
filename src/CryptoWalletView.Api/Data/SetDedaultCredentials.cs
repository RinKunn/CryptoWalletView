using Binance.Net.Clients;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CryptoWalletView.Api.Data;

public static class DedaultCredentials
{
    public static void SetBinanceDedaultCredentials(this IServiceProvider serviceProvider)
    {
        using (var context = new CryptoContext(serviceProvider
            .GetRequiredService<DbContextOptions<CryptoContext>>()))
        {
            var api = context.Apis.FirstOrDefault();
            if(api == null)
                throw new Exception("Not found any api credentials");
            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                LogLevel = LogLevel.Debug,
                ApiCredentials = new ApiCredentials(api.Key, api.Secret),
            });
        }
    }
}