using Microsoft.AspNetCore.Mvc;
using PodcastAppAPI.DTO;
using PodcastAppAPI.Interface;
using PodcastAppAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(PodcastAppAPI.DTO.RegisterRequest request)
    {
        try
        {
            if (_authService.EmailExists(request.Email))
                return BadRequest(new { message = "User already exists." });

            _authService.RegisterUser(request);
            return Ok(new { message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Server error: " + ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = _authService.GetUserIfValid(request);

        if (user == null)
            return Unauthorized(new { message = "Invalid credentials." });

        return Ok(new
        {
            message = "Login successful.",
            userId = user.UserId   
        });
    }

}
