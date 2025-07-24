using System.ComponentModel.DataAnnotations;

namespace PodcastApp.DTO
{
    public record EpisodeDTO
    (
        [Required]
        int PodcastId,

        [Required]
        [MaxLength(100)]
        string? Title,

        [Required]
        [MaxLength(500)]
        string? Description,

        [Required]
        [Url]
        string? AudioUrl,

        [Required]
        [RegularExpression(@"^\d{1,2}:\d{2}:\d{2}$", ErrorMessage = "Duration must be in HH:mm:ss format.")]
        string Duration,

        [Required]
        [DataType(DataType.Date)]
        DateTime ReleaseDate
    );
}

