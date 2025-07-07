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

        public void AddEpisode(Episode episode)
        {
            _context.Episodes.Add(episode);
            _context.SaveChanges();
        }

        public void UpdateEpisode(Episode episode)
        {
            _context.Episodes.Update(episode);
            _context.SaveChanges();
        }

        public void DeleteEpisode(Episode episode)
        {
            _context.Episodes.Remove(episode);
            _context.SaveChanges();
        }

        public Episode? GetEpisodeById(int episodeId)
        {
            return _context.Episodes.FirstOrDefault(e => e.EpisodeId == episodeId);
        }

        public List<Episode> GetEpisodesByPodcast(int podcastId)
        {
            return _context.Episodes
                .Where(e => e.PodcastId == podcastId)
                .OrderByDescending(e => e.ReleaseDate)
                .ToList();
        }
    }
}
