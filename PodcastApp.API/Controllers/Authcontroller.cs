using Microsoft.AspNetCore.Mvc;
using PodcastApp.DTO;
using PodcastApp.Interface;


namespace PodcastApp.API.Controllers
{
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
        public IActionResult Register([FromBody] RegisterRequest request)
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
        public IActionResult Login([FromBody] LoginRequest login)
        {
            var user = _authService.GetUserIfValid(login);

            if (user == null)
                return Unauthorized(new { message = "Invalid credentials." });

            if (user.IsSuspended)
                return Unauthorized(new { message = "Your account has been suspended. Please contact support." });

            return Ok(new
            {
                userId = user.UserId,
                role = user.Role,
                isSuspended = user.IsSuspended
            });
        }
    }
}
