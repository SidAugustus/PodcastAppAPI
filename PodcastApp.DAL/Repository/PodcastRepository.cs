namespace PodcastApp.DAL.Repository;
using PodcastApp.DAL.Infrastructure;
using PodcastApp.Core.Interface;
using PodcastApp.Core.Models;

public class PodcastRepository : IPodcastRepository
{
    private readonly AppDbContext _context;

    public PodcastRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Podcast podcast)
    {
        _context.Podcasts.Add(podcast);
        _context.SaveChanges();
    }

    public List<Podcast> GetAll() => _context.Podcasts.ToList();
    public List<Podcast> GetUnapproved() =>
        _context.Podcasts.Where(p => !p.IsApproved).ToList();

    public void Approve(int podcastId)
    {
        var podcast = _context.Podcasts.FirstOrDefault(p => p.PodcastId == podcastId);
        if (podcast != null)
        {
            podcast.IsApproved = true;
            _context.SaveChanges();
        }
    }
}
