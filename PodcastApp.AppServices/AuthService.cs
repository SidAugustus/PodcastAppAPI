using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.AppServices
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _unitOfWork.Users.GetUserByEmailAsync(email) != null;
        }

        public async Task RegisterUserAsync(RegisterRequest request)
        {
            var hashedPassword = HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = hashedPassword,
                RoleId = request.RoleId,
                IsFlagged = false,
                IsSuspended = false
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> ValidateLoginAsync(LoginRequest request)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
            if (user == null) return false;

            return VerifyPassword(request.Password, user.PasswordHash);
        }

        public async Task<User?> GetUserIfValidAsync(LoginRequest request)
        {
            var user = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
            if (user == null) return null;

            return VerifyPassword(request.Password, user.PasswordHash) ? user : null;
        }

        public string HashPassword(string password)
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

