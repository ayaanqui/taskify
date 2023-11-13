using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    [HttpPost]
    public IActionResult Home()
    {
        return Ok(new { Message = "Taskify API v1.0.0" });
    }
}
