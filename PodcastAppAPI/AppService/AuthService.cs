using System.Security.Cryptography;
using System.Text;
using PodcastAppAPI.Models;
using PodcastAppAPI.Interface;

namespace PodcastAppAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool EmailExists(string email)
        {
            return _userRepository.GetUserByEmail(email) != null;
        }

        public void RegisterUser(DTO.RegisterRequest request)
        {
            var hashedPassword = HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = hashedPassword,
                RoleId = request.RoleId == 1 ? 2 : request.RoleId
            };

            _userRepository.AddUser(user);
        }

        public bool ValidateLogin(DTO.LoginRequest request)
        {
            var user = _userRepository.GetUserByEmail(request.Email);
            if (user == null) return false;

            return VerifyPassword(request.Password, user.PasswordHash);
        }

        public User GetUserIfValid(DTO.LoginRequest request)
        {
            var user = _userRepository.GetUserByEmail(request.Email);
            if (user == null) return null;

            return VerifyPassword(request.Password, user.PasswordHash) ? user : null;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string plainText, string hashed)
        {
            var hash = HashPassword(plainText);
            return hash == hashed;
        }
    }
}
