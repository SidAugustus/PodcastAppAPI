using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PodcastApp.DTO.Attributes;

namespace PodcastApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [SmartRequired]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [SmartRequired]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [SmartRequired]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
        
        [SmartRequired]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@*_]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, include at least one uppercase letter, one lowercase letter, one number, and one special character (#, @, *, _).")]
        public string PasswordHash { get; set; } = string.Empty;

        [SmartRequired]
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        [SmartRequired]
        public bool IsSuspended { get; set; } = false;

        [SmartRequired]
        public bool IsFlagged { get; set; } = false;


        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }


    }

}