using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.AppServices
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IPodcastRepository _podcastRepository;

        public EpisodeService(IEpisodeRepository episodeRepository, IPodcastRepository podcastRepository)
        {
            _episodeRepository = episodeRepository;
            _podcastRepository = podcastRepository;
        }

        public bool AddEpisode(EpisodeDTO dto)
        {
            var podcast = _podcastRepository.GetPodcastById(dto.PodcastId);
            if (podcast == null) return false;

            var episode = new Episode
            {
                PodcastId = dto.PodcastId,
                Title = dto.Title,
                Description = dto.Description,
                AudioUrl = dto.AudioUrl,
                Duration = dto.Duration,
                ReleaseDate = dto.ReleaseDate
            };

            _episodeRepository.AddEpisode(episode);
            return true;
        }

        public bool UpdateEpisode(int episodeId, EpisodeDTO dto)
        {
            var episode = _episodeRepository.GetEpisodeById(episodeId);
            if (episode == null) return false;

            episode.Title = dto.Title;
            episode.Description = dto.Description;
            episode.AudioUrl = dto.AudioUrl;
            episode.Duration = dto.Duration;

            _episodeRepository.UpdateEpisode(episode);
            return true;
        }

        public bool DeleteEpisode(int episodeId)
        {
            var episode = _episodeRepository.GetEpisodeById(episodeId);
            if (episode == null) return false;

            _episodeRepository.DeleteEpisode(episode);
            return true;
        }

        public List<Episode> GetEpisodesByPodcast(int podcastId)
        {
            return _episodeRepository.GetEpisodesByPodcast(podcastId);
        }
    }
}
