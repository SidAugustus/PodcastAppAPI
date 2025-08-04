using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface ISubscriptionService
    {
        Task SubscribeAsync(SubscriptionDTO dto);
        Task UnsubscribeAsync(SubscriptionDTO dto);
        Task<List<Subscription>?> GetSubscriptionsByUserAsync(int userId);
    }
}
