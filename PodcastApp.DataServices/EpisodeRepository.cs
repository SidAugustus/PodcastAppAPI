using PodcastApp.Interface;
using PodcastApp.Models;
using Microsoft.EntityFrameworkCore;

namespace PodcastApp.Repository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext _context;

        public EpisodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddEpisodeAsync(Episode episode)
        {
            await _context.Episodes.AddAsync(episode);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEpisodeAsync(Episode episode)
        {
            _context.Episodes.Update(episode);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEpisodeAsync(Episode episode)
        {
            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();
        }

        public async Task<Episode?> GetEpisodeByIdAsync(int episodeId)
        {
            return await _context.Episodes.FirstOrDefaultAsync(e => e.EpisodeId == episodeId);
        }

        public async Task<List<Episode>> GetEpisodesByPodcastAsync(int podcastId)
        {
            return await _context.Episodes
                .Where(e => e.PodcastId == podcastId)
                .OrderByDescending(e => e.ReleaseDate)
                .ToListAsync();
        }
    }
}
