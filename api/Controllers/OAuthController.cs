using System.Security.Claims;
using api.Models.Dto;
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
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { Message = "Unauthorized" });
        }

        var email = User.FindFirstValue(ClaimTypes.Email);
        var name = User.FindFirstValue(ClaimTypes.Name);
        var user = this.userRepository.FindOrCreate(new UserDto
        {
            FullName = name,
            Email = email,
        });

        return Ok(new
        {
            Me = user,
            OAuthSource = User.Identity.AuthenticationType,
            Token = "MY_JWT_TOKEN",
        });
    }
}
