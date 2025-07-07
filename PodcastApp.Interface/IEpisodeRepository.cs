using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IEpisodeRepository
    {
        void AddEpisode(Episode episode);
        void UpdateEpisode(Episode episode);
        void DeleteEpisode(Episode episode);
        Episode? GetEpisodeById(int episodeId);
        List<Episode> GetEpisodesByPodcast(int podcastId);
    }
}
