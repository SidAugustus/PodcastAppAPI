using Microsoft.AspNetCore.Mvc;
using PodcastApp.DTO;
using PodcastApp.Interface;

namespace PodcastApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;

        public EpisodeController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpPost("add")]
        public IActionResult AddEpisode([FromBody] EpisodeDTO dto)
        {
            try
            {
                _episodeService.AddEpisode(dto);
                return Ok(new { message = " Episode added successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateEpisode(int id, [FromBody] EpisodeDTO dto)
        {
            try
            {
                _episodeService.UpdateEpisode(id, dto);
                return Ok(new { message = " Episode updated successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteEpisode(int id)
        {
            try
            {
                _episodeService.DeleteEpisode(id);
                return Ok(new { message = " Episode deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        
        [HttpGet("byPodcast/{podcastId}")]
        public IActionResult GetEpisodesByPodcast(int podcastId)
        {
            try
            {
                var episodes = _episodeService.GetEpisodesByPodcast(podcastId);
                return Ok(episodes);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
