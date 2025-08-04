using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IEpisodeRepository : IGenericRepository<Episode>
    {
        Task<List<Episode>?> GetEpisodesByPodcastAsync(int podcastId);
        Task<Episode?> GetEpisodeByIdAsync(int episodeId);

        Task<List<Episode>?> GetApprovedEpisodesByPodcastPaginatedAsync(int podcastId, int skip, int take);
        Task<int> GetApprovedEpisodeCountAsync(int podcastId);

    }
}

