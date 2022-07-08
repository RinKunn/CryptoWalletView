using CryptoWalletView.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWalletView.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MarketController : ControllerBase
{
    private readonly IMarketDataService _marketDataService;

    public MarketController(IMarketDataService marketDataService)
    {
        _marketDataService = marketDataService ?? throw new ArgumentNullException(nameof(marketDataService));
    }

    [HttpGet("{coinPair}")]
    public async Task<IActionResult> Index(string coinPair)
    {
        if(string.IsNullOrEmpty(coinPair))
            return BadRequest("Please, type coin pair, like BTCUSDT");
        try
        {
            var res = await _marketDataService.GetTodayCandlesForSymbols(new string[] {coinPair});
            return Ok(res);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
