using Microsoft.AspNetCore.Mvc;
using QuizPop.Models.DTO;
using QuizPop.Services;
using QuizPop.Tools;

namespace QuizPop.API;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var user = _userService.GetUser(loginRequest);
        if (user == null) return Unauthorized();

        var token = JwtTokenGenerator.GenerateToken(user);
        return Ok(new { Token = token });
    }
}