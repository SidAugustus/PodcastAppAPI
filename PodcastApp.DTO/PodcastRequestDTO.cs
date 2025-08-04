using System.ComponentModel.DataAnnotations;
using PodcastApp.DTO.Attributes;

namespace PodcastApp.DTO
{
    public record PodcastRequestDTO
    (
        [SmartRequired]
        [MaxLength(100)]
        string Title,

        [SmartRequired]
        [MaxLength(500)]
        string Description,

        [SmartRequired]
        [MaxLength(100)]
        string Category,

        [SmartRequired]
        int CreatedByUserId

    );
}
