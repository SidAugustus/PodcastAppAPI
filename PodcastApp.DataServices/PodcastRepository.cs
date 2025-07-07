using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.Repository
{
    public class PodcastRepository : IPodcastRepository
    {
        private readonly AppDbContext _context;

        public PodcastRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddPodcast(Podcast podcast)
        {
            _context.Podcasts.Add(podcast);
            _context.SaveChanges();
        }

        public Podcast? GetPodcastById(int podcastId)
        {
            return _context.Podcasts.FirstOrDefault(p => p.PodcastId == podcastId);
        }

        public void UpdatePodcast(Podcast podcast)
        {
            _context.Podcasts.Update(podcast);
            _context.SaveChanges();
        }

        public void DeletePodcast(Podcast podcast)
        {
            _context.Podcasts.Remove(podcast);
            _context.SaveChanges();
        }

        public List<Podcast> GetPodcastsByApprovalStatus(bool isApproved)
        {
            return _context.Podcasts
                .Where(p => p.IsApproved == isApproved)
                .ToList();
        }

        public List<Podcast> GetFlaggedPodcasts()
        {
            return _context.Podcasts
                .Where(p => p.IsFlagged)
                .ToList();
        }

        public List<Podcast> GetPodcastsByUser(int userId)
        {
            return _context.Podcasts
                .Where(p => p.CreatedByUserId == userId)
                .ToList();
        }

        public bool HasOtherFlaggedPodcasts(int userId)
        {
            return _context.Podcasts
                .Any(p => p.CreatedByUserId == userId && p.IsFlagged);
        }

        public List<object> GetMinimalApprovedPodcasts()
        {
            return _context.Podcasts
                .Where(p => p.IsApproved)
                .Select(p => new
                {
                    p.PodcastId,
                    p.Title,
                    p.Description,
                    p.Category
                })
                .Cast<object>()
                .ToList();
        }
    }
}
