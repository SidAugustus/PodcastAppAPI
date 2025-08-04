using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task SubscribeAsync(int userId, int podcastId);
        Task UnsubscribeAsync(int userId, int podcastId);
        Task<List<Subscription>?> GetSubscriptionsByUserAsync(int userId);
    }
}

