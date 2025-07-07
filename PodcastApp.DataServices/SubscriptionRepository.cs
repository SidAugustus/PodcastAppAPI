using Microsoft.EntityFrameworkCore;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Subscribe(int userId, int podcastId)
        {
            var alreadySubscribed = _context.Subscriptions
                .Any(s => s.UserId == userId && s.PodcastId == podcastId);

            if (!alreadySubscribed)
            {
                _context.Subscriptions.Add(new Subscription { UserId = userId, PodcastId = podcastId });
                _context.SaveChanges();
            }
        }

        public void Unsubscribe(int userId, int podcastId)
        {
            var subscription = _context.Subscriptions
                .FirstOrDefault(s => s.UserId == userId && s.PodcastId == podcastId);

            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                _context.SaveChanges();
            }
        }

        public List<Subscription> GetSubscriptionsByUser(int userId)
        {
            return _context.Subscriptions
                .Where(s => s.UserId == userId)
                .Include(s => s.Podcast)  // used to include podcast details
                .ToList();
        }
    }

}
