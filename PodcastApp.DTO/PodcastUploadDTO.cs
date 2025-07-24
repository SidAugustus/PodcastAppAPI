using System.ComponentModel.DataAnnotations;

namespace PodcastApp.DTO
{
    public record PodcastUploadDTO
    (
        [Required]
        [MaxLength(100)]
        string Title,

        [Required]
        [MaxLength(500)]
        string Description,

        [Required]
        [MaxLength(100)]
        string Category,

        [Required]
        int CreatedByUserId
    );
}
