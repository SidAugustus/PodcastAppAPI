using PodcastApp.Models;
using System.Security.Claims;

namespace PodcastApp.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);

        string CreateRefreshToken(User user);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);


    }
}
