using System.ComponentModel.DataAnnotations;
using PodcastApp.DTO.Attributes;

namespace PodcastApp.DTO
{
    public record EpisodeDTO
    (
        [SmartRequired]
        int PodcastId,

        [SmartRequired]
        [MaxLength(100)]
        string? Title,

        [SmartRequired]
        [MaxLength(500)]
        string? Description,

        [SmartRequired]
        [Url]
        string? AudioUrl,

        [SmartRequired]
        [RegularExpression(@"^\d{1,2}:\d{2}:\d{2}$", ErrorMessage = "Duration must be in HH:mm:ss format.")]
        string Duration,

        [SmartRequired]
        [DataType(DataType.Date)]
        DateTime ReleaseDate
    );
}


