using PodcastApp.Core.Models;

namespace PodcastApp.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public byte[]? ProfilePicture { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

}