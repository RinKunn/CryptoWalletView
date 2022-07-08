using Binance.Net.Clients;
using Binance.Net.Interfaces.Clients;
using CryptoWalletView.Api.Data;
using CryptoWalletView.Api.Services;
using CryptoWalletView.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CryptoContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CryptoContext") 
        ?? throw new InvalidOperationException("Connection string 'CryptoContext' not found.")));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddScoped<IBinanceClient, BinanceClient>();
builder.Services.AddScoped<ICoinInfoService, CoinInfoService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IMarketDataService, MarketDataService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    services.SetBinanceDedaultCredentials();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

app.UseAuthorization();

app.MapControllers();

app.Run();
