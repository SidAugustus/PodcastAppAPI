using System.ComponentModel.DataAnnotations;

namespace PodcastApp.DTO
{
    public record RegisterRequest
    (
        [Required] [MaxLength(100)]
        string FirstName,   
        
        [Required] [MaxLength(100)]
        string LastName,

        [Required] [MaxLength(100)]
        string Email,

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@*_]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, include at least one uppercase letter, one lowercase letter, one number, and one special character (#, @, *, _).")]
        string Password,

        [Required] [MaxLength(100)]
        string PhoneNumber,

        [Required]
        int RoleId       
    );
}
