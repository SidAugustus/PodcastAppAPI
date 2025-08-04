using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastApp.API.Controllers;
using PodcastApp.DTO;
using PodcastApp.Interface;

namespace PodcastApp.API
{
    [Authorize]
    [ApiVersion("1.0")]  // This marks this controller for v1.0
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PodcastController : ControllerBase
    {
        private readonly IPodcastService _podcastService;
        private readonly ILogger<PodcastController> _logger;

        public PodcastController(IPodcastService podcastService, ILogger<PodcastController> logger)
        {
            _podcastService = podcastService;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPodcastAsync([FromBody] PodcastUploadDTO dto)
        {
            _logger.LogInformation("Uploading Podcast: {Title} ", dto.Title);
            var result = await _podcastService.UploadPodcastAsync(dto);
            return Ok(result);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingPodcastsAsync()
        {

            var result = await _podcastService.GetPendingPodcastsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "1")]
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApprovePodcastAsync(int id)
        {
            _logger.LogInformation($"Approval attempt for Podcast {id}");
            var success = await _podcastService.ApprovePodcastAsync(id);
            if (!success) return NotFound(new { message = "Podcast not found." });

            return Ok(new { message = "Podcast approved." });
        }

        [Authorize(Roles = "1")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePodcastAsync(int id)
        {
            _logger.LogInformation($"delete attempt for: {id}");
            var success = await _podcastService.DeletePodcastAsync(id);
            if (!success) return NotFound(new { message = "Podcast not found." });

            return Ok(new { message = "Podcast deleted." });
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetApprovedPodcastsAsync()
        {
            var result = await _podcastService.GetApprovedPodcastsAsync();
            return Ok(result);
        }

        [HttpGet("flagged")]
        public async Task<IActionResult> GetFlaggedPodcastsAsync()
        {
            var result = await _podcastService.GetFlaggedPodcastsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "1")]
        [HttpPut("flag/{id}")]
        public async Task<IActionResult> FlagPodcastAsync(int id)
        {
            _logger.LogInformation($"attempt to flag podcast: {id}");
            var success = await _podcastService.FlagPodcastAndUserAsync(id);
            if (!success) return NotFound(new { message = "Podcast not found." });

            return Ok(new { message = "Podcast and user flagged." });
        }

        [Authorize(Roles = "1")]
        [HttpPut("unflag/{id}")]
        public async Task<IActionResult> UnflagPodcastAndUserAsync(int id)
        {
            _logger.LogInformation($"attempt to unflag podcast: {id}");
            var success = await _podcastService.UnflagPodcastAndUserAsync(id);
            if (!success) return NotFound(new { message = "Podcast not found." });

            return Ok(new { message = "Podcast and user unflagged." });
        }

        [HttpGet("user/flagged")]
        public async Task<IActionResult> GetFlaggedUsersAsync()
        {
            var result = await _podcastService.GetFlaggedUsersAsync();
            return Ok(result);
        }

        [Authorize(Roles = "1")]
        [HttpPut("user/suspend/{id}")]
        public async Task<IActionResult> SuspendUserAsync(int id)
        {
            _logger.LogInformation($"attempt to suspend user {id}");
            var success = await _podcastService.SuspendUserAsync(id);
            if (!success) return NotFound(new { message = "User not found." });

            return Ok(new { message = "User suspended." });
        }

        [Authorize(Roles = "1")]
        [HttpPut("user/unsuspend/{id}")]
        public async Task<IActionResult> UnsuspendUserAsync(int id)
        {
            _logger.LogInformation($"attempt to unsuspend user {id}");
            var success = await _podcastService.UnsuspendUserAsync(id);
            if (!success) return NotFound(new { message = "User not found." });

            return Ok(new { message = "User unsuspended." });
        }

        [HttpGet("user/suspended")]
        public async Task<IActionResult> GetSuspendedUsersAsync()
        {
            var result = await _podcastService.GetSuspendedUsersAsync();
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPodcastsByUserAsync(int userId)
        {
            var result = await _podcastService.GetPodcastsByUserAsync(userId);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult?> GetAllPodcastsAsync()
        {
            var result = await _podcastService.GetAllApprovedPodcastsAsync();
            return Ok(result);
        }

        [HttpGet("approved/paginated")]
        public async Task<IActionResult?> GetPaginatedApprovedPodcastsAsync(int page = 1, int pageSize = 5)
        {
            var (podcasts, totalCount) = await _podcastService.GetApprovedPodcastsPaginatedAsync(page, pageSize);

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling((double)totalCount! / pageSize),
                data = podcasts
            });
        }
    }
}



