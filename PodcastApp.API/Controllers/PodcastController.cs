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
    public IActionResult UploadPodcast([FromBody] PodcastUploadDTO dto)
    {
        var result = _podcastService.UploadPodcast(dto);
        return Ok(result);
    }

    [HttpGet("pending")]
    public IActionResult GetPendingPodcasts()
    {
        var result = _podcastService.GetPendingPodcasts();
        return Ok(result);
    }

    [HttpPut("approve/{id}")]
    public IActionResult ApprovePodcast(int id)
    {
        var success = _podcastService.ApprovePodcast(id);
        if (!success) return NotFound(new { message = "Podcast not found." });

        return Ok(new { message = "Podcast approved." });
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeletePodcast(int id)
    {
        var success = _podcastService.DeletePodcast(id);
        if (!success) return NotFound(new { message = "Podcast not found." });

        return Ok(new { message = "Podcast deleted." });
    }

    [HttpGet("approved")]
    public IActionResult GetApprovedPodcasts()
    {
        var result = _podcastService.GetApprovedPodcasts();
        return Ok(result);
    }

    [HttpGet("flagged")]
    public IActionResult GetFlaggedPodcasts()
    {
        var result = _podcastService.GetFlaggedPodcasts();
        return Ok(result);
    }

    [HttpPut("flag/{id}")]
    public IActionResult FlagPodcast(int id)
    {
        var success = _podcastService.FlagPodcastAndUser(id);
        if (!success) return NotFound(new { message = "Podcast not found." });

        return Ok(new { message = "Podcast and user flagged." });
    }

    [HttpPut("unflag/{id}")]
    public IActionResult UnflagPodcastAndUser(int id)
    {
        var success = _podcastService.UnflagPodcastAndUser(id);
        if (!success) return NotFound(new { message = "Podcast not found." });

        return Ok(new { message = "Podcast and user unflagged." });
    }

    [HttpGet("user/flagged")]
    public IActionResult GetFlaggedUsers()
    {
        var result = _podcastService.GetFlaggedUsers();
        return Ok(result);
    }

    [HttpPut("user/suspend/{id}")]
    public IActionResult SuspendUser(int id)
    {
        var success = _podcastService.SuspendUser(id);
        if (!success) return NotFound(new { message = "User not found." });

        return Ok(new { message = "User suspended." });
    }

    [HttpPut("user/unsuspend/{id}")]
    public IActionResult UnsuspendUser(int id)
    {
        var success = _podcastService.UnsuspendUser(id);
        if (!success) return NotFound(new { message = "User not found." });

        return Ok(new { message = "User unsuspended." });
    }

    [HttpGet("user/suspended")]
    public IActionResult GetSuspendedUsers()
    {
        var result = _podcastService.GetSuspendedUsers();
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetPodcastsByUser(int userId)
    {
        var result = _podcastService.GetPodcastsByUser(userId);
        return Ok(result);
    }

    [HttpGet("all")]
    public IActionResult GetAllPodcasts()
    {
        var result = _podcastService.GetAllApprovedPodcasts();
        return Ok(result);
    }

    [HttpGet("approved/paginated")]
    public IActionResult GetPaginatedApprovedPodcasts(int page = 1, int pageSize = 5)
    {
        var query = _podcastService.GetApprovedPodcasts(); // Full list

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



