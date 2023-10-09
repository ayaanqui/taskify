using System.Security.Claims;
using api.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("oauth")]
public class OAuthController : ControllerBase
{
    private readonly UserRepository userRepository;

    public OAuthController(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet("google")]
    [AllowAnonymous]
    public IActionResult GoogleSignin()
    {
        var authenticationProperties = new AuthenticationProperties
        {
            // RedirectUri = "/google-signin",
            RedirectUri = Url.Action("GenerateJwt"),
        };

        return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("generate-jwt")]
    [Authorize]
    public IActionResult GenerateJwt()
    {
        // await HttpContext.SignInAsync(GoogleDefaults.AuthenticationScheme, new ClaimsPrincipal());
        return Ok(new
        {
            Me = new
            {
                Fullname = User.FindFirstValue(ClaimTypes.Name),
                Email = User.FindFirstValue(ClaimTypes.Email),
                DOB = User.FindFirstValue(ClaimTypes.DateOfBirth)
            },
            Token = "MY_JWT_TOKEN",
        });
    }
}
