using Microsoft.AspNetCore.Mvc;

namespace CryptoWalletView.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WalletController : ControllerBase
{
    public IActionResult Index() 
    {
        return Ok();
    }
}
