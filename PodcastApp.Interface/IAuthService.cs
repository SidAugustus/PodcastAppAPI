using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface

{
    public interface IAuthService
    {
        Task<bool> EmailExistsAsync(string email);
        Task RegisterUserAsync(RegisterRequest request);
        Task<bool> ValidateLoginAsync(LoginRequest request);
        string HashPassword(string Password);
        Task<User?> GetUserIfValidAsync(LoginRequest request);
    }
}
