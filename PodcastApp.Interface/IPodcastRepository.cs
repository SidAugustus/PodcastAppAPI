using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IPodcastRepository
    {
        void AddPodcast(Podcast podcast);
        Podcast? GetPodcastById(int podcastId);
        void UpdatePodcast(Podcast podcast);
        void DeletePodcast(Podcast podcast);

        List<Podcast> GetPodcastsByApprovalStatus(bool isApproved);
        List<Podcast> GetFlaggedPodcasts();
        List<Podcast> GetPodcastsByUser(int userId);
        bool HasOtherFlaggedPodcasts(int userId);

        List<object> GetMinimalApprovedPodcasts();
    }
}
