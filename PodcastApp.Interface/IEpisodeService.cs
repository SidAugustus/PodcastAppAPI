using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IEpisodeService
    {
        bool AddEpisode(EpisodeDTO dto);
        bool UpdateEpisode(int episodeId, EpisodeDTO dto);
        bool DeleteEpisode(int episodeId);
        List<Episode> GetEpisodesByPodcast(int podcastId);
    }
}

