using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IEpisodeRepository
    {
        Task AddEpisodeAsync(Episode episode);
        Task UpdateEpisodeAsync(Episode episode);
        Task DeleteEpisodeAsync(Episode episode);
        Task<Episode?> GetEpisodeByIdAsync(int episodeId);
        Task<List<Episode>> GetEpisodesByPodcastAsync(int podcastId);
    }
}
