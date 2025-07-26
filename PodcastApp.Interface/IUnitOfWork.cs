using System.Threading.Tasks;
using PodcastApp.Models;

namespace PodcastApp.Interface
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPodcastRepository Podcasts { get; }
        IEpisodeRepository Episodes { get; }
        ISubscriptionRepository Subscriptions { get; }

        Task<int> CompleteAsync();
    }
}
