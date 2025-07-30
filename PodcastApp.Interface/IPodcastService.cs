using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IPodcastService
    {
        Task<bool> UploadPodcastAsync(PodcastUploadDTO dto);

        Task<List<Podcast>> GetPendingPodcastsAsync();
        Task<List<Podcast>> GetApprovedPodcastsAsync();
        Task<List<Podcast>> GetFlaggedPodcastsAsync();
        Task<List<Podcast>> GetPodcastsByUserAsync(int userId);

        Task<List<User>> GetFlaggedUsersAsync();
        Task<List<User>> GetSuspendedUsersAsync();

        Task<List<object>> GetAllApprovedPodcastsAsync();

        Task<bool> ApprovePodcastAsync(int id);
        Task<bool> DeletePodcastAsync(int id);
        Task<bool> FlagPodcastAndUserAsync(int podcastId);
        Task<bool> UnflagPodcastAndUserAsync(int podcastId);
        Task<bool> SuspendUserAsync(int userId);
        Task<bool> UnsuspendUserAsync(int userId);

        //Task<PaginatedList<PodcastDTO>> GetPaginatedApprovedAsync(PaginationParams paginationParams);
    }
}
