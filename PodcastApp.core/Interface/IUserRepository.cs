using PodcastApp.Core.Models;

namespace PodcastApp.Core.Interface
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetUserByEmail(string email);
    }
}
