using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.AppServices
{
    public class PodcastService : IPodcastService
    {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IUserRepository _userRepository;

        public PodcastService(IPodcastRepository podcastRepository, IUserRepository userRepository)
        {
            _podcastRepository = podcastRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> UploadPodcastAsync(PodcastUploadDTO dto)
        {
            var podcast = new Podcast
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = DateTime.Now,
                IsApproved = false
            };

            await _podcastRepository.AddPodcastAsync(podcast);
            return true;
        }

        public async Task<List<Podcast>> GetPendingPodcastsAsync()
        {
            return await _podcastRepository.GetPodcastsByApprovalStatusAsync(false);
        }

        public async Task<bool> ApprovePodcastAsync(int id)
        {
            var podcast = await _podcastRepository.GetPodcastByIdAsync(id);
            if (podcast == null) return false;

            podcast.IsApproved = true;
            await _podcastRepository.UpdatePodcastAsync(podcast);
            return true;
        }

        public async Task<bool> DeletePodcastAsync(int id)
        {
            var podcast = await _podcastRepository.GetPodcastByIdAsync(id);
            if (podcast == null) return false;

            await _podcastRepository.DeletePodcastAsync(podcast);
            return true;
        }

        public async Task<bool> FlagPodcastAndUserAsync(int podcastId)
        {
            var podcast = await _podcastRepository.GetPodcastByIdAsync(podcastId);
            if (podcast == null) return false;

            podcast.IsFlagged = true;
            await _podcastRepository.UpdatePodcastAsync(podcast);

            var user = await _userRepository.GetUserByIdAsync(podcast.CreatedByUserId);
            if (user != null)
            {
                user.IsFlagged = true;
                await _userRepository.UpdateUserAsync(user);
            }

            return true;
        }

        public async Task<bool> UnflagPodcastAndUserAsync(int podcastId)
        {
            var podcast = await _podcastRepository.GetPodcastByIdAsync(podcastId);
            if (podcast == null) return false;

            podcast.IsFlagged = false;
            await _podcastRepository.UpdatePodcastAsync(podcast);

            var user = await _userRepository.GetUserByIdAsync(podcast.CreatedByUserId);
            if (user != null)
            {
                bool hasOtherFlagged = await _podcastRepository.HasOtherFlaggedPodcastsAsync(user.UserId);
                if (!hasOtherFlagged)
                {
                    user.IsFlagged = false;
                    await _userRepository.UpdateUserAsync(user);
                }
            }

            return true;
        }

        public async Task<List<Podcast>> GetFlaggedPodcastsAsync()
        {
            return await _podcastRepository.GetFlaggedPodcastsAsync();
        }

        public async Task<List<Podcast>> GetApprovedPodcastsAsync()
        {
            return await _podcastRepository.GetPodcastsByApprovalStatusAsync(true);
        }

        public async Task<List<object>> GetAllApprovedPodcastsAsync()
        {
            return await _podcastRepository.GetMinimalApprovedPodcastsAsync();
        }

        public async Task<List<Podcast>> GetPodcastsByUserAsync(int userId)
        {
            return await _podcastRepository.GetPodcastsByUserAsync(userId);
        }

        public async Task<List<User>> GetFlaggedUsersAsync()
        {
            return await _userRepository.GetFlaggedUsersAsync();
        }

        public async Task<List<User>> GetSuspendedUsersAsync()
        {
            return await _userRepository.GetSuspendedUsersAsync();
        }

        public async Task<bool> SuspendUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.IsSuspended = true;
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> UnsuspendUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.IsSuspended = false;
            await _userRepository.UpdateUserAsync(user);
            return true;
        }
    }
}
