using PodcastApp.Core.Models;

namespace PodcastApp.Core.Interface
{
    public interface IEpisodeRepository
    {
        void AddEpisode(Episode episode);
        List<Episode> GetEpisodesByPodcast(int podcastId);
    }
}
