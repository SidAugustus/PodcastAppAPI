using PodcastAppAPI.Models;
using PodcastAppAPI.DTO;

namespace PodcastAppAPI.Interface

{
    public interface IAuthService
    {
        bool EmailExists(string email);
        void RegisterUser(RegisterRequest request);
        bool ValidateLogin(LoginRequest request);
        User GetUserIfValid(LoginRequest request);
    }
}
