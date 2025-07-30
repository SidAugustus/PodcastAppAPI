using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodcastApp.AppServices;
using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;
using System.Security.Claims;

namespace PodcastApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.RefreshToken);
            if (principal == null)
            {
                return Unauthorized(new { message = "Invalid refresh token" });
            }

            // We assume the token is valid and hasn't expired
            var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;

            // Recreate user object from claims (for token generation)
            var user = new User { UserId = userId, Email = email! };

            var newAccessToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.CreateRefreshToken(user);

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
    }
}
