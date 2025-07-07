using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IPodcastService
    {
        bool UploadPodcast(PodcastUploadDTO dto);

        List<Podcast> GetPendingPodcasts();
        List<Podcast> GetApprovedPodcasts();
        List<Podcast> GetFlaggedPodcasts();
        List<Podcast> GetPodcastsByUser(int userId);

        List<User> GetFlaggedUsers();
        List<User> GetSuspendedUsers();

        List<object> GetAllApprovedPodcasts();

        bool ApprovePodcast(int id);
        bool DeletePodcast(int id);
        bool FlagPodcastAndUser(int podcastId);
        bool UnflagPodcastAndUser(int podcastId);
        bool SuspendUser(int userId);
        bool UnsuspendUser(int userId);
    }
}
