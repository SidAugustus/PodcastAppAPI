using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.AppServices
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        public async Task SubscribeAsync(SubscriptionDTO dto)
        {
            await _unitOfWork.Subscriptions.SubscribeAsync(dto.UserId, dto.PodcastId);
        }

        public async Task UnsubscribeAsync(SubscriptionDTO dto)
        {
            await _unitOfWork.Subscriptions.UnsubscribeAsync(dto.UserId, dto.PodcastId);
        }

        public async Task<List<Subscription>> GetSubscriptionsByUserAsync(int userId)
        {
            return await _unitOfWork.Subscriptions.GetSubscriptionsByUserAsync(userId);
        }
    }
}

