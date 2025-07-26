using PodcastApp.DTO;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.AppServices
{
    public class PodcastService : IPodcastService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PodcastService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.Podcasts.AddAsync(podcast);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<Podcast>> GetPendingPodcastsAsync()
        {
            return await _unitOfWork.Podcasts.GetPodcastsByApprovalStatusAsync(false);
        }

        public async Task<bool> ApprovePodcastAsync(int id)
        {
            var podcast = await _unitOfWork.Podcasts.GetByIdAsync(id);
            if (podcast == null) return false;

            podcast.IsApproved = true;
            _unitOfWork.Podcasts.Update(podcast);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeletePodcastAsync(int id)
        {
            var podcast = await _unitOfWork.Podcasts.GetByIdAsync(id);
            if (podcast == null) return false;

            _unitOfWork.Podcasts.Delete(podcast);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> FlagPodcastAndUserAsync(int podcastId)
        {
            var podcast = await _unitOfWork.Podcasts.GetByIdAsync(podcastId);
            if (podcast == null) return false;

            podcast.IsFlagged = true;
            _unitOfWork.Podcasts.Update(podcast);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Users.GetByIdAsync(podcast.CreatedByUserId);
            if (user != null)
            {
                user.IsFlagged = true;
                _unitOfWork.Users.Update(user);
                await _unitOfWork.CompleteAsync();
            }

            return true;
        }

        public async Task<bool> UnflagPodcastAndUserAsync(int podcastId)
        {
            var podcast = await _unitOfWork.Podcasts.GetByIdAsync(podcastId);
            if (podcast == null) return false;

            podcast.IsFlagged = false;
            _unitOfWork.Podcasts.Update(podcast);
            await _unitOfWork.CompleteAsync();

            var user = await _unitOfWork.Users.GetByIdAsync(podcast.CreatedByUserId);
            if (user != null)
            {
                var hasOtherFlagged = await _unitOfWork.Podcasts.HasOtherFlaggedPodcastsAsync(user.UserId);
                if (!hasOtherFlagged)
                {
                    user.IsFlagged = false;
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.CompleteAsync();
                }
            }

            return true;
        }

        public async Task<List<Podcast>> GetFlaggedPodcastsAsync()
        {
            return await _unitOfWork.Podcasts.GetFlaggedPodcastsAsync();
        }

        public async Task<List<Podcast>> GetApprovedPodcastsAsync()
        {
            return await _unitOfWork.Podcasts.GetPodcastsByApprovalStatusAsync(true);
        }

        public async Task<List<object>> GetAllApprovedPodcastsAsync()
        {
            return await _unitOfWork.Podcasts.GetMinimalApprovedPodcastsAsync();
        }

        public async Task<List<Podcast>> GetPodcastsByUserAsync(int userId)
        {
            return await _unitOfWork.Podcasts.GetPodcastsByUserAsync(userId);
        }

        public async Task<List<User>> GetFlaggedUsersAsync()
        {
            return await _unitOfWork.Users.GetFlaggedUsersAsync();
        }

        public async Task<List<User>> GetSuspendedUsersAsync()
        {
            return await _unitOfWork.Users.GetSuspendedUsersAsync();
        }

        public async Task<bool> SuspendUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null) return false;

            user.IsSuspended = true;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UnsuspendUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null) return false;

            user.IsSuspended = false;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}

