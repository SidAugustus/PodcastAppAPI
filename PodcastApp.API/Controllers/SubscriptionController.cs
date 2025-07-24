using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodcastApp.DTO;
using PodcastApp.Interface;

namespace PodcastApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserSubscriptionsAsync(int userId)
        {
            var subscriptions = await _subscriptionService.GetSubscriptionsByUserAsync(userId);

            if (subscriptions == null || !subscriptions.Any())
                return NotFound(new { message = "No subscriptions found for the user." });

            return Ok(subscriptions);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeAsync([FromBody] SubscriptionDTO dto)
        {
            if (dto == null || dto.UserId <= 0 || dto.PodcastId <= 0)
                return BadRequest(new { message = "Invalid subscription data." });

            await _subscriptionService.SubscribeAsync(dto);

            return Ok(new { message = "✅ Subscribed successfully." });
        }


        [HttpPost("unsubscribe")]
        public async Task<IActionResult> UnsubscribeAsync([FromBody] SubscriptionDTO dto)
        {
            if (dto == null || dto.UserId <= 0 || dto.PodcastId <= 0)
                return BadRequest(new { message = "Invalid subscription data." });

            await _subscriptionService.UnsubscribeAsync(dto);

            return Ok(new { message = "✅ Unsubscribed successfully." });
        }

    }
}
