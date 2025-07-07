using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface ISubscriptionService
    {
        void Subscribe(SubscriptionDTO dto);
        void Unsubscribe(SubscriptionDTO dto);
        List<Subscription> GetSubscriptionsByUser(int userId);
    }
}
