using AutoMapper;
using Microsoft.Extensions.Logging;
using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace PodcastApp.AppServices
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;   
        private readonly ILogger<AuthService> _logger;
        private readonly ITokenService _tokenService;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AuthService> logger, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _unitOfWork.Users.GetUserByEmailAsync(email) != null;
        }

        public async Task RegisterUserAsync(RegisterRequest request)
        {
            _logger.LogInformation($"Registering user: {request.FirstName} {request.LastName}");
            var user = _mapper.Map<User>(request);
            user.PasswordHash = HashPassword(request.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> ValidateLoginAsync(LoginRequest request)
        {
            _logger.LogInformation($"Validating Login of {request.Email}"); 
            var user = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
            if (user == null) return false;

            return VerifyPassword(request.Password, user.PasswordHash);
        }

        public async Task<User?> GetUserIfValidAsync(LoginRequest request)
        {
            _logger.LogInformation($"Checking if User {request.Email} is a registered user.");
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

        public AuthResponseDTO GenerateTokens(User user)
        {
            return new AuthResponseDTO
            {
                AccessToken = _tokenService.CreateToken(user),
                RefreshToken = _tokenService.CreateRefreshToken(user),
                UserId = user.UserId,
                Role = user.RoleId
            };
        }

    }
}

