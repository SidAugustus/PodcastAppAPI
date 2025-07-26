using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IEpisodeRepository : IGenericRepository<Episode>
    {
        Task<List<Episode>> GetEpisodesByPodcastAsync(int podcastId);
        Task<Episode?> GetEpisodeByIdAsync(int episodeId);
    }
}

