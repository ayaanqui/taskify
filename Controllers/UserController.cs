using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("users")]
public class WeatherForecastController : ControllerBase
{
    private readonly UserRepository userRepository;

    public WeatherForecastController(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet(Name = "GetAllUsers")]
    public IEnumerable<User> Get()
    {
        return this.userRepository.FindAll();
    }
}
