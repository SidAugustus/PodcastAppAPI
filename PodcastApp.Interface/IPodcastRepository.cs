using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IPodcastRepository : IGenericRepository<Podcast>
    {
        Task<List<Podcast>> GetPodcastsByApprovalStatusAsync(bool isApproved);
        Task<List<Podcast>> GetFlaggedPodcastsAsync();
        Task<List<Podcast>> GetPodcastsByUserAsync(int userId);
        Task<bool> HasOtherFlaggedPodcastsAsync(int userId);
        Task<List<object>> GetMinimalApprovedPodcastsAsync();
    }
}

