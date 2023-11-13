using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserRepository userRepository;

    public UserController(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpGet(Name = "GetAllUsers")]
    public IEnumerable<User> GetAllUsers() => this.userRepository.FindAll();

    [AllowAnonymous]
    [HttpGet("{userId}")]
    public User GetUser(int userId)
    {
        return this.userRepository.FindById(userId) ?? throw new Exception("could not find user");
    }
}
