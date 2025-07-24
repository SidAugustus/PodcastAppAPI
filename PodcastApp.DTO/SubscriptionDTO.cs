using System.ComponentModel.DataAnnotations;

namespace PodcastApp.DTO
{
    public record SubscriptionDTO
    (
        [Required]
        int UserId,

        [Required]
        int PodcastId
    );
}
