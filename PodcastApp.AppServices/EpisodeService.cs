using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.AppServices
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EpisodeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddEpisodeAsync(EpisodeDTO dto)
        {
            var podcast = await _unitOfWork.Podcasts.GetByIdAsync(dto.PodcastId);
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

            await _unitOfWork.Episodes.AddAsync(episode);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateEpisodeAsync(int episodeId, EpisodeDTO dto)
        {
            var episode = await _unitOfWork.Episodes.GetByIdAsync(episodeId);
            if (episode == null) return false;

            episode.Title = dto.Title;
            episode.Description = dto.Description;
            episode.AudioUrl = dto.AudioUrl;
            episode.Duration = dto.Duration;

            _unitOfWork.Episodes.Update(episode);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteEpisodeAsync(int episodeId)
        {
            var episode = await _unitOfWork.Episodes.GetByIdAsync(episodeId);
            if (episode == null) return false;

            _unitOfWork.Episodes.Delete(episode);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<Episode>> GetEpisodesByPodcastAsync(int podcastId)
        {
            return await _unitOfWork.Episodes.GetEpisodesByPodcastAsync(podcastId);
        }
    }
}
