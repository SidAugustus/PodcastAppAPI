using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IPodcastRepository
    {
        Task AddPodcastAsync(Podcast podcast);
        Task<Podcast?> GetPodcastByIdAsync(int podcastId);
        Task UpdatePodcastAsync(Podcast podcast);
        Task DeletePodcastAsync(Podcast podcast);

        Task<List<Podcast>> GetPodcastsByApprovalStatusAsync(bool isApproved);
        Task<List<Podcast>> GetFlaggedPodcastsAsync();
        Task<List<Podcast>> GetPodcastsByUserAsync(int userId);
        Task<bool> HasOtherFlaggedPodcastsAsync(int userId);

        Task<List<object>> GetMinimalApprovedPodcastsAsync();
    }
}
