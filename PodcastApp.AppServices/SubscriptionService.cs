using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.AppServices
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _repository;

        public SubscriptionService(ISubscriptionRepository repository)
        {
            _repository = repository;
        }

        public void Subscribe(SubscriptionDTO dto)
        {
            _repository.Subscribe(dto.UserId, dto.PodcastId);
        }

        public void Unsubscribe(SubscriptionDTO dto)
        {
            _repository.Unsubscribe(dto.UserId, dto.PodcastId);
        }

        public List<Subscription> GetSubscriptionsByUser(int userId)
        {
            return _repository.GetSubscriptionsByUser(userId);
        }
    }
}
