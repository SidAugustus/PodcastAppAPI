using AutoMapper;
using Microsoft.Extensions.Logging;
using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.AppServices
{
    public class EpisodeService : IEpisodeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<EpisodeService> _logger;

        public EpisodeService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<EpisodeService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddEpisodeAsync(EpisodeDTO dto)
        {
            _logger.LogInformation($"Adding Episode {dto.Title} to {dto.PodcastId}");
            var podcast = await _unitOfWork.Podcasts.GetByIdAsync(dto.PodcastId);
            if (podcast == null) return false;

            var episode = _mapper.Map<Episode>(dto);

            await _unitOfWork.Episodes.AddAsync(episode);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateEpisodeAsync(int episodeId, EpisodeDTO dto)
        {
            _logger.LogInformation($"Updating the Episode titled: {dto.Title}");
            var episode = await _unitOfWork.Episodes.GetByIdAsync(episodeId);
            if (episode == null) return false;
            
            _mapper.Map(dto, episode);

            _unitOfWork.Episodes.Update(episode);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteEpisodeAsync(int episodeId)
        {
            _logger.LogInformation($"deleting episode {episodeId}");
            var episode = await _unitOfWork.Episodes.GetByIdAsync(episodeId);
            if (episode == null) return false;

            _unitOfWork.Episodes.Delete(episode);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<Episode>> GetEpisodesByPodcastAsync(int podcastId)
        {
            _logger.LogInformation($"Getting Episodes of the Podcast: {podcastId}");
            return await _unitOfWork.Episodes.GetEpisodesByPodcastAsync(podcastId);
        }
    }
}
