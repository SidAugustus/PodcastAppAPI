using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetUserByEmail(string email);
        User? GetUserById(int userId);
        List<User> GetAllUsers();
        List<User> GetFlaggedUsers();
        List<User> GetSuspendedUsers();
        void UpdateUser(User user);
    }
}
