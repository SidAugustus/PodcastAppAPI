using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodcastApp.DTO;
using PodcastApp.Interface;

namespace PodcastApp.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]  // This marks this controller for v1.0
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ISubscriptionService subscriptionService, ILogger<SubscriptionController> logger)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserSubscriptionsAsync(int userId)
        {
            _logger.LogInformation($"attempt to get subscriptions of user {userId}");
            var subscriptions = await _subscriptionService.GetSubscriptionsByUserAsync(userId);

            if (subscriptions == null || !subscriptions.Any())
                return NotFound(new { message = "No subscriptions found for the user." });

            return Ok(subscriptions);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeAsync([FromBody] SubscriptionDTO dto)
        {
            _logger.LogInformation($"attempt to subscribe to {dto.PodcastId}"); 
            if (dto == null || dto.UserId <= 0 || dto.PodcastId <= 0)
                return BadRequest(new { message = "Invalid subscription data." });

            await _subscriptionService.SubscribeAsync(dto);

            return Ok(new { message = "✅ Subscribed successfully." });
        }


        [HttpPost("unsubscribe")]
        public async Task<IActionResult> UnsubscribeAsync([FromBody] SubscriptionDTO dto)
        {
            _logger.LogInformation($"attempt to unsubscribe from {dto.PodcastId}");
            if (dto == null || dto.UserId <= 0 || dto.PodcastId <= 0)
                return BadRequest(new { message = "Invalid subscription data." });

            await _subscriptionService.UnsubscribeAsync(dto);

            return Ok(new { message = "✅ Unsubscribed successfully." });
        }

    }
}
