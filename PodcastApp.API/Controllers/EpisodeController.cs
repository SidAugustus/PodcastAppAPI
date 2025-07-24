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
        public async Task<IActionResult> AddEpisodeAsync([FromBody] EpisodeDTO dto)
        {
            try
            {
                await _episodeService.AddEpisodeAsync(dto);
                return Ok(new { message = " Episode added successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEpisodeAsync(int id, [FromBody] EpisodeDTO dto)
        {
            try
            {
                await _episodeService.UpdateEpisodeAsync(id, dto);
                return Ok(new { message = " Episode updated successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEpisodeAsync(int id)
        {
            try
            {
                await _episodeService.DeleteEpisodeAsync(id);
                return Ok(new { message = " Episode deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        
        [HttpGet("byPodcast/{podcastId}")]
        public async Task<IActionResult> GetEpisodesByPodcastAsync(int podcastId)
        {
            try
            {
                var episodes = await _episodeService.GetEpisodesByPodcastAsync(podcastId);
                return Ok(episodes);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
