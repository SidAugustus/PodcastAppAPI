using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface ISubscriptionRepository
    {
        void Subscribe(int userId, int podcastId);
        void Unsubscribe(int userId, int podcastId);
        List<Subscription> GetSubscriptionsByUser(int userId);
    }

}
