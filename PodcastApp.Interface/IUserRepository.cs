using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int userId);
        Task<List<User>?> GetAllUsersAsync();
        Task<List<User>?> GetFlaggedUsersAsync();
        Task<List<User>?> GetSuspendedUsersAsync();
        Task UpdateUserAsync(User user);
    }
}
