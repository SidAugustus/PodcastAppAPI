using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodcastApp.AppServices;
using PodcastApp.DTO;
using PodcastApp.Interface;


namespace PodcastApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly ITokenService _tokenService;

       
        public AuthController(IAuthService authService, ILogger<AuthController> logger, ITokenService tokenService)
        {
            _authService = authService;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            _logger.LogInformation("Registeration attempt for User: {FirstName} {LastName}", request.FirstName, request.LastName);  
             if (await _authService.EmailExistsAsync(request.Email))
                return BadRequest(new { message = "User already exists." });

             await _authService.RegisterUserAsync(request);
             return Ok(new { message = "User registered successfully." });
            
          
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            //throw new Exception("Simulated failure in login."); //to check if filter exception handling is working.
            _logger.LogInformation("Login Attempt for: {Email} ", login.Email);
            var user = await _authService.GetUserIfValidAsync(login);

            if (user == null)
                return Unauthorized(new { message = "Invalid credentials." });

            if (user.IsSuspended)
                return Unauthorized(new { message = "Your account has been suspended. Please contact support." });

            var tokenResponse = _authService.GenerateTokens(user);

            return Ok(tokenResponse);
        }
    }
}
