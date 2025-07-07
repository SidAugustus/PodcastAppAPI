using Microsoft.AspNetCore.Mvc;
using PodcastAppAPI.Data;
using PodcastAppAPI.Models;
using PodcastAppAPI.DTO;

[ApiController]
[Route("api/[controller]")]
public class PodcastController : ControllerBase
{
    private readonly AppDbContext _context;

    public PodcastController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("pending")]
    public IActionResult GetPendingPodcasts()
    {
        var pending = _context.Podcasts.Where(p => !p.IsApproved).ToList();
        return Ok(pending);
    }

    [HttpPut("approve/{id}")]
    public IActionResult ApprovePodcast(int id)
    {
        var podcast = _context.Podcasts.Find(id);
        if (podcast == null)
            return NotFound(new { message = "Podcast not found." });

        podcast.IsApproved = true;
        _context.SaveChanges();
        return Ok(new { message = "Podcast approved." });
    }
}


