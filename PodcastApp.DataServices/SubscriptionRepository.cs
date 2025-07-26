using Microsoft.EntityFrameworkCore;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.Repository
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task SubscribeAsync(int userId, int podcastId)
        {
            bool alreadySubscribed = await _context.Subscriptions
                .AnyAsync(s => s.UserId == userId && s.PodcastId == podcastId);

            if (!alreadySubscribed)
            {
                await _context.Subscriptions.AddAsync(new Subscription
                {
                    UserId = userId,
                    PodcastId = podcastId
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnsubscribeAsync(int userId, int podcastId)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.PodcastId == podcastId);

            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Subscription>> GetSubscriptionsByUserAsync(int userId)
        {
            return await _context.Subscriptions
                .Where(s => s.UserId == userId)
                .Include(s => s.Podcast)
                .ToListAsync();
        }
    }
}

