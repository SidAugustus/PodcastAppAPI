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
        private readonly ILogger<EpisodeController> _logger;

        public EpisodeController(IEpisodeService episodeService, ILogger<EpisodeController> logger)
        {
            _episodeService = episodeService;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEpisodeAsync([FromBody] EpisodeDTO dto)
        {
            _logger.LogInformation($"attempt to add episode {dto.Title} to {dto.PodcastId}");
            
            await _episodeService.AddEpisodeAsync(dto);
            return Ok(new { message = " Episode added successfully." });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEpisodeAsync(int id, [FromBody] EpisodeDTO dto)
        {
            _logger.LogInformation($"attempt to update episode {dto.Title}");
            
            await _episodeService.UpdateEpisodeAsync(id, dto);
            return Ok(new { message = " Episode updated successfully." });
            
           
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEpisodeAsync(int id)
        {
          _logger.LogInformation($"attempt to delete episode {id}");    
          
          await _episodeService.DeleteEpisodeAsync(id);
          return Ok(new { message = " Episode deleted successfully." });
           
        }
        
        [HttpGet("byPodcast/{podcastId}")]
        public async Task<IActionResult> GetEpisodesByPodcastAsync(int podcastId)
        {
            _logger.LogInformation($"attempt to get epiosdes of podcast: {podcastId}");
            var episodes = await _episodeService.GetEpisodesByPodcastAsync(podcastId);
            return Ok(episodes);
           
        }
    }
}
