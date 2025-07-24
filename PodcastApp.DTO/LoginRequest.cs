using System.ComponentModel.DataAnnotations;

namespace PodcastApp.DTO
{
    public record LoginRequest
    (
        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@*_]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, include at least one uppercase letter, one lowercase letter, one number, and one special character (#, @, *, _).")]
        string Password
    );
}
