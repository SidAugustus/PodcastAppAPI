using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PodcastApp.AppServices;
using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;
using PodcastApp.Repository;
using Serilog;
using System.Reflection.Metadata;

namespace PodcastUnitTests
{
    public class AuthServiceTest
    {
        public class AuthServiceTests
        {
            private readonly Mock<IUserRepository> _mockUserRepo = new();
            private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();
            private readonly Mock<IMapper> _mockMapper = new();
            private readonly Mock<ITokenService> _mockTokenService = new();
            private readonly Mock<ILogger<AuthService>> _mockLogger = new();

            private readonly AuthService _authService;

            public AuthServiceTests()
            {
                //_mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);
                _authService = new AuthService(
                    _mockUnitOfWork.Object,
                    _mockMapper.Object,
                    _mockLogger.Object,
                    _mockTokenService.Object
                );
            }


            [Fact]
            public async Task EmailExistsAsync_ShouldReturnTrue_WhenUserExists()
            {
                // Arrange
                var email = "sonnycorleone@gmail.com";
                _mockUnitOfWork.Setup(u => u.Users.GetUserByEmailAsync(email))
                    .ReturnsAsync(new User());

                // Act
                var result = await _authService.EmailExistsAsync(email);

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public async Task Register_UserAsync_Should_Add_User_And_Commit()
            {
                // Arrange
                var request = new RegisterRequestDTO(
                    "Santino",
                    "Corleone",
                    "SonnyCorleone@gmail.com",
                    "Mancini_111",
                    "1234567890",
                    2
                );

                var user = new User();

                _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);
                _mockMapper.Setup(m => m.Map<User>(request)).Returns(user);

                // Act
                await _authService.RegisterUserAsync(request);

                // Assert
                user.PasswordHash.Should().NotBeNullOrWhiteSpace("because the password should be hashed before saving the user");

                _mockUserRepo.Verify(r => r.AddAsync(user), Times.Once);
                _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
            }

            [Fact]
            public async Task ValidateLoginAsync_ShouldReturnFalse_WhenUserNotFound()
            {
                // Arrange
                var request = new LoginRequestDTO ( "sonnycorleone@gmail.com", "Mancini_111" );
                _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);
                _mockUserRepo.Setup(r => r.GetUserByEmailAsync(request.Email)).ReturnsAsync((User?)null);

                // Act
                var result = await _authService.ValidateLoginAsync(request);

                // Assert
                result.Should().BeFalse();
            }

        

            [Fact]
            public async Task ValidateLoginAsync_ShouldReturnTrue_WhenPasswordIsCorrect()
            {
                // Arrange
                var request = new LoginRequestDTO ("sonnycorelone@gmail.com","Mancini_111" );
                var hashed = _authService.HashPassword(request.Password);
                var user = new User { PasswordHash = hashed };

                _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object); 
                _mockUserRepo.Setup(r => r.GetUserByEmailAsync(request.Email)).ReturnsAsync(user);

                // Act
                var result = await _authService.ValidateLoginAsync(request);

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public async Task GetUserIfValidAsync_ReturnsUser_WhenValid()
            {
                var login = new LoginRequestDTO ("michaelcorleone@gmail.com","Appollonia_10" );
                var user = new User { Email = login.Email, PasswordHash = _authService.HashPassword(login.Password) };

                _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);
                _mockUserRepo.Setup(r => r.GetUserByEmailAsync(login.Email)).ReturnsAsync(user);

                var result = await _authService.GetUserIfValidAsync(login);

                result.Should().Be(user);
            }

            [Fact]
            public void GenerateTokens_ShouldReturnTokenDto_WithExpectedValues()
            {
                // Arrange
                var user = new User { UserId = 1, RoleId = 2 };

                _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);
                _mockTokenService.Setup(t => t.CreateToken(user)).Returns("access_token");
                _mockTokenService.Setup(t => t.CreateRefreshToken(user)).Returns("refresh_token");

                // Act
                var result = _authService.GenerateTokens(user);

                // Assert
                result.AccessToken.Should().Be("access_token");
                result.RefreshToken.Should().Be("refresh_token");
                result.UserId.Should().Be(1);
                result.Role.Should().Be(2);
            }
        }
    }
}
