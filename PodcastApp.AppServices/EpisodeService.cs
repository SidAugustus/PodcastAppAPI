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

        public async Task<bool> AddEpisodeAsync(EpisodeDTO dto)
        {
            var podcast = await _podcastRepository.GetPodcastByIdAsync(dto.PodcastId);
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

            await _episodeRepository.AddEpisodeAsync(episode);
            return true;
        }

        public async Task<bool> UpdateEpisodeAsync(int episodeId, EpisodeDTO dto)
        {
            var episode = await _episodeRepository.GetEpisodeByIdAsync(episodeId);
            if (episode == null) return false;

            episode.Title = dto.Title;
            episode.Description = dto.Description;
            episode.AudioUrl = dto.AudioUrl;
            episode.Duration = dto.Duration;

            await _episodeRepository.UpdateEpisodeAsync(episode);
            return true;
        }

        public async Task<bool> DeleteEpisodeAsync(int episodeId)
        {
            var episode = await _episodeRepository.GetEpisodeByIdAsync(episodeId);
            if (episode == null) return false;

            await _episodeRepository.DeleteEpisodeAsync(episode);
            return true;
        }

        public async Task<List<Episode>> GetEpisodesByPodcastAsync(int podcastId)
        {
            return await _episodeRepository.GetEpisodesByPodcastAsync(podcastId);
        }
    }
}
