using System.ComponentModel.DataAnnotations;
using PodcastApp.DTO.Attributes;
namespace PodcastApp.DTO
{
    public record RegisterRequestDTO
    (
        [SmartRequired] [MaxLength(100)]
        string FirstName,   
        
        [SmartRequired] [MaxLength(100)]
        string LastName,

        [SmartRequired] [MaxLength(100)]
        string Email,

        [SmartRequired]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@*_]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, include at least one uppercase letter, one lowercase letter, one number, and one special character (#, @, *, _).")]
        string Password,

        [SmartRequired] [MaxLength(100)]
        string PhoneNumber,

        [SmartRequired]
        int RoleId       
    );
}
