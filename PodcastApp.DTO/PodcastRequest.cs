using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PodcastApp.DTO
{
    public record PodcastRequest
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
