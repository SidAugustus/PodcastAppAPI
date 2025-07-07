using PodcastApp.Core.Interface;
using PodcastApp.Core.Models;
using PodcastApp.DAL.Infrastructure;

namespace PodcastApp.DAL.Repository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly AppDbContext _context;

        public EpisodeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddEpisode(Episode episode)
        {
            _context.Episodes.Add(episode);
            _context.SaveChanges();
        }

        public List<Episode> GetEpisodesByPodcast(int podcastId)
        {
            return _context.Episodes
                .Where(e => e.PodcastId == podcastId)
                .ToList();
        }
    }
}
