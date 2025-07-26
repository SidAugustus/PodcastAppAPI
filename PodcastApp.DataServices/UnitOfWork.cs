using PodcastApp.Interface;
using PodcastApp.Models;
using System.Security.AccessControl;


namespace PodcastApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public IPodcastRepository Podcasts { get; }
        public IEpisodeRepository Episodes { get; }
        public ISubscriptionRepository Subscriptions { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Podcasts = new PodcastRepository(_context);
            Episodes = new EpisodeRepository(_context);
            Subscriptions = new SubscriptionRepository(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}

