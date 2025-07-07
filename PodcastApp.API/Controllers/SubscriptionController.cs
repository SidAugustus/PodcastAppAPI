using Microsoft.AspNetCore.Mvc;
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

        // 🔍 Get all subscriptions of a user
        [HttpGet("user/{userId}")]
        public IActionResult GetUserSubscriptions(int userId)
        {
            var subscriptions = _subscriptionService.GetSubscriptionsByUser(userId);

            if (subscriptions == null || !subscriptions.Any())
                return NotFound(new { message = "No subscriptions found for the user." });

            return Ok(subscriptions);
        }

        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] SubscriptionDTO dto)
        {
            if (dto == null || dto.UserId <= 0 || dto.PodcastId <= 0)
                return BadRequest(new { message = "Invalid subscription data." });

            _subscriptionService.Subscribe(dto);

            return Ok(new { message = "✅ Subscribed successfully." });
        }


        [HttpPost("unsubscribe")]
        public IActionResult Unsubscribe([FromBody] SubscriptionDTO dto)
        {
            if (dto == null || dto.UserId <= 0 || dto.PodcastId <= 0)
                return BadRequest(new { message = "Invalid subscription data." });

            _subscriptionService.Unsubscribe(dto);

            return Ok(new { message = "✅ Unsubscribed successfully." });
        }

    }
}
