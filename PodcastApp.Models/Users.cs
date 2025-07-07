namespace PodcastApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsSuspended { get; set; } = false;
        public bool IsFlagged { get; set; } = false;


        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

}