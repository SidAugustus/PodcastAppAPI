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

        public async Task SubscribeAsync(SubscriptionDTO dto)
        {
            await _repository.SubscribeAsync(dto.UserId, dto.PodcastId);
        }

        public async Task UnsubscribeAsync(SubscriptionDTO dto)
        {
            await _repository.UnsubscribeAsync(dto.UserId, dto.PodcastId);
        }

        public async Task<List<Subscription>> GetSubscriptionsByUserAsync(int userId)
        {
            return await _repository.GetSubscriptionsByUserAsync(userId);
        }
    }
}
