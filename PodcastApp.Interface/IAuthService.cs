using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface

{
    public interface IAuthService
    {
        Task<bool> EmailExistsAsync(string email);
        Task RegisterUserAsync(RegisterRequestDTO request);
        Task<bool> ValidateLoginAsync(LoginRequestDTO request);
        string HashPassword(string Password);
        Task<User?> GetUserIfValidAsync(LoginRequestDTO request);

        AuthResponseDTO GenerateTokens(User user);


    }

}



