using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface

{
    public interface IAuthService
    {
        bool EmailExists(string email);
        void RegisterUser(RegisterRequest request);
        bool ValidateLogin(LoginRequest request);
        string HashPassword(string Password);
        User GetUserIfValid(LoginRequest request);
    }
}
