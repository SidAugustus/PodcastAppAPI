using Microsoft.EntityFrameworkCore;
using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.Repository
{
    public class EpisodeRepository : GenericRepository<Episode>, IEpisodeRepository
    {
        private readonly AppDbContext _context;

        public EpisodeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Episode>?> GetEpisodesByPodcastAsync(int podcastId)
        {
            return await _context.Episodes
                .Where(e => e.PodcastId == podcastId)
                .OrderByDescending(e => e.ReleaseDate)
                .ToListAsync();
        }

        public async Task<Episode?> GetEpisodeByIdAsync(int episodeId)
        {
            return await _context.Episodes.FirstOrDefaultAsync(e => e.EpisodeId == episodeId);
        }

        public async Task<List<Episode>?> GetApprovedEpisodesByPodcastPaginatedAsync(int podcastId, int skip, int take)
        {
            return await _context.Episodes
                .Where(e => e.PodcastId == podcastId)
                .OrderBy(e => e.ReleaseDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetApprovedEpisodeCountAsync(int podcastId)
        {
            return await _context.Episodes
                .CountAsync(e => e.PodcastId == podcastId);
        }

    }
}
