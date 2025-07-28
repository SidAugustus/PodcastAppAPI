using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;
using Microsoft.Extensions.Logging;

namespace PodcastApp.AppServices
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SubscriptionService> _logger;


        public SubscriptionService(IUnitOfWork unitOfWork, ILogger<SubscriptionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task SubscribeAsync(SubscriptionDTO dto)
        {
            _logger.LogInformation($"{dto.UserId} subscribing to {dto.PodcastId}");
            await _unitOfWork.Subscriptions.SubscribeAsync(dto.UserId, dto.PodcastId);
        }

        public async Task UnsubscribeAsync(SubscriptionDTO dto)
        {
            _logger.LogInformation($"{dto.UserId} unsubscribing from {dto.PodcastId}");
            await _unitOfWork.Subscriptions.UnsubscribeAsync(dto.UserId, dto.PodcastId);
        }

        public async Task<List<Subscription>> GetSubscriptionsByUserAsync(int userId)
        {
            _logger.LogInformation($"getting list of subscriptions of User: {userId}");
            return await _unitOfWork.Subscriptions.GetSubscriptionsByUserAsync(userId);
        }
    }
}

