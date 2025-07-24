using Microsoft.AspNetCore.Mvc;
using PodcastApp.DTO;
using PodcastApp.Interface;

[ApiController]
[Route("api/[controller]")]
public class PodcastController : ControllerBase
{
    private readonly IPodcastService _podcastService;

    public PodcastController(IPodcastService podcastService)
    {
        _podcastService = podcastService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadPodcastAsync([FromBody] PodcastUploadDTO dto)
    {
        var result = await _podcastService.UploadPodcastAsync(dto);
        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingPodcastsAsync()
    {
        var result = await _podcastService.GetPendingPodcastsAsync();
        return Ok(result);
    }

    [HttpPut("approve/{id}")]
    public async Task<IActionResult> ApprovePodcastAsync(int id)
    {
        var success = await _podcastService.ApprovePodcastAsync(id);
        if (!success) return NotFound(new { message = "Podcast not found." });

        return Ok(new { message = "Podcast approved." });
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePodcastAsync(int id)
    {
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

    [HttpPut("flag/{id}")]
    public async Task<IActionResult> FlagPodcastAsync(int id)
    {
        var success = await _podcastService.FlagPodcastAndUserAsync(id);
        if (!success) return NotFound(new { message = "Podcast not found." });

        return Ok(new { message = "Podcast and user flagged." });
    }

    [HttpPut("unflag/{id}")]
    public async Task<IActionResult> UnflagPodcastAndUserAsync(int id)
    {
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

    [HttpPut("user/suspend/{id}")]
    public async Task<IActionResult> SuspendUserAsync(int id)
    {
        var success = await _podcastService.SuspendUserAsync(id);
        if (!success) return NotFound(new { message = "User not found." });

        return Ok(new { message = "User suspended." });
    }

    [HttpPut("user/unsuspend/{id}")]
    public async Task<IActionResult> UnsuspendUserAsync(int id)
    {
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
    public async Task<IActionResult> GetAllPodcastsAsync()
    {
        var result = await _podcastService.GetAllApprovedPodcastsAsync();
        return Ok(result);
    }

    [HttpGet("approved/paginated")]
    public async Task<IActionResult> GetPaginatedApprovedPodcastsAsync(int page = 1, int pageSize = 5)
    {
        var query = await _podcastService.GetApprovedPodcastsAsync(); // Full list

        var total = query.Count;
        var paginated = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return Ok(new
        {
            totalCount = total,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling((double)total / pageSize),
            data = paginated
        });
    }


}



