using System.ComponentModel.DataAnnotations;
using PodcastApp.DTO.Attributes;

namespace PodcastApp.DTO
{
    public record SubscriptionDTO
    (
        [SmartRequired]
        int UserId,

        [SmartRequired]
        int PodcastId
    );
}
