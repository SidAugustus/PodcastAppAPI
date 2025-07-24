using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IEpisodeService
    {
        Task<bool> AddEpisodeAsync(EpisodeDTO dto);
        Task<bool> UpdateEpisodeAsync(int episodeId, EpisodeDTO dto);
        Task<bool> DeleteEpisodeAsync(int episodeId);
        Task<List<Episode>> GetEpisodesByPodcastAsync(int podcastId);
    }
}

