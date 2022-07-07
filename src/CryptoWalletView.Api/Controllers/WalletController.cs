using CryptoWalletView.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWalletView.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var res = await _walletService.GetWalletinfo();
            return Ok(res);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
