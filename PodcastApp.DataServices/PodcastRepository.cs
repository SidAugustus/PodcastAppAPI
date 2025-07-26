using Microsoft.EntityFrameworkCore;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.Repository
{
    public class PodcastRepository : GenericRepository<Podcast>, IPodcastRepository
    {
        private readonly AppDbContext _context;

        public PodcastRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Podcast>> GetPodcastsByApprovalStatusAsync(bool isApproved)
        {
            return await _context.Podcasts
                .Where(p => p.IsApproved == isApproved)
                .ToListAsync();
        }

        public async Task<List<Podcast>> GetFlaggedPodcastsAsync()
        {
            return await _context.Podcasts
                .Where(p => p.IsFlagged)
                .ToListAsync();
        }

        public async Task<List<Podcast>> GetPodcastsByUserAsync(int userId)
        {
            return await _context.Podcasts
                .Where(p => p.CreatedByUserId == userId)
                .ToListAsync();
        }

        public async Task<bool> HasOtherFlaggedPodcastsAsync(int userId)
        {
            return await _context.Podcasts
                .AnyAsync(p => p.CreatedByUserId == userId && p.IsFlagged);
        }

        public async Task<List<object>> GetMinimalApprovedPodcastsAsync()
        {
            return await _context.Podcasts
                .Where(p => p.IsApproved)
                .Select(p => new
                {
                    p.PodcastId,
                    p.Title,
                    p.Description,
                    p.Category
                })
                .Cast<object>()
                .ToListAsync();
        }
    }
}
