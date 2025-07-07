using PodcastApp.Core.Models;
using PodcastApp.Core.DTO;

namespace PodcastApp.Core.Interface

{
    public interface IAuthService
    {
        bool EmailExists(string email);
        void RegisterUser(RegisterRequest request);
        bool ValidateLogin(LoginRequest request);
        User GetUserIfValid(LoginRequest request);
    }
}
