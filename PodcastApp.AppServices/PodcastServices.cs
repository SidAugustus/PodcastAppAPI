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

        public bool UploadPodcast(PodcastUploadDTO dto)
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

            _podcastRepository.AddPodcast(podcast);
            return true; // ✅ Ensure method returns a boolean
        }

        public List<Podcast> GetPendingPodcasts()
        {
            return _podcastRepository.GetPodcastsByApprovalStatus(false);
        }

        public bool ApprovePodcast(int id)
        {
            var podcast = _podcastRepository.GetPodcastById(id);
            if (podcast == null) return false;

            podcast.IsApproved = true;
            _podcastRepository.UpdatePodcast(podcast);
            return true;
        }

        public bool DeletePodcast(int id)
        {
            var podcast = _podcastRepository.GetPodcastById(id);
            if (podcast == null) return false;

            _podcastRepository.DeletePodcast(podcast);
            return true;
        }

        public bool FlagPodcastAndUser(int podcastId)
        {
            var podcast = _podcastRepository.GetPodcastById(podcastId);
            if (podcast == null) return false;

            podcast.IsFlagged = true;
            _podcastRepository.UpdatePodcast(podcast);

            var user = _userRepository.GetUserById(podcast.CreatedByUserId);
            if (user != null)
            {
                user.IsFlagged = true;
                _userRepository.UpdateUser(user);
            }

            return true;
        }

        public bool UnflagPodcastAndUser(int podcastId)
        {
            var podcast = _podcastRepository.GetPodcastById(podcastId);
            if (podcast == null) return false;

            podcast.IsFlagged = false;
            _podcastRepository.UpdatePodcast(podcast);

            var user = _userRepository.GetUserById(podcast.CreatedByUserId);
            if (user != null)
            {
                bool hasOtherFlagged = _podcastRepository.HasOtherFlaggedPodcasts(user.UserId);
                if (!hasOtherFlagged)
                {
                    user.IsFlagged = false;
                    _userRepository.UpdateUser(user);
                }
            }

            return true;
        }

        public List<Podcast> GetFlaggedPodcasts()
        {
            return _podcastRepository.GetFlaggedPodcasts();
        }

        public List<Podcast> GetApprovedPodcasts()
        {
            return _podcastRepository.GetPodcastsByApprovalStatus(true);
        }

        public List<object> GetAllApprovedPodcasts()
        {
            return _podcastRepository.GetMinimalApprovedPodcasts();
        }

        public List<Podcast> GetPodcastsByUser(int userId)
        {
            return _podcastRepository.GetPodcastsByUser(userId);
        }

        public List<User> GetFlaggedUsers()
        {
            return _userRepository.GetFlaggedUsers();
        }

        public List<User> GetSuspendedUsers()
        {
            return _userRepository.GetSuspendedUsers();
        }

        public bool SuspendUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null) return false;

            user.IsSuspended = true;
            _userRepository.UpdateUser(user);
            return true;
        }

        public bool UnsuspendUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null) return false;

            user.IsSuspended = false;
            _userRepository.UpdateUser(user);
            return true;
        }
    }
}
