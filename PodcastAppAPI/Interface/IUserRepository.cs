using PodcastAppAPI.Models;

namespace PodcastAppAPI.Interface
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetUserByEmail(string email);
    }
}
