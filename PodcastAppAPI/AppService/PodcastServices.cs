using PodcastAppAPI.DTO;
using PodcastAppAPI.Interface;
using PodcastAppAPI.Models;
using PodcastAppAPI.Repositories;

public class PodcastService : IPodcastService
{
    private readonly IPodcastRepository _repo;

    public PodcastService(IPodcastRepository repo)
    {
        _repo = repo;
    }

    public void CreatePodcast(PodcastRequest request)
    {
        var podcast = new Podcast
        {
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            CreatedByUserId = request.CreatedByUserId,
            IsApproved = false,
            CreatedAt = DateTime.UtcNow
        };
        _repo.Add(podcast);
    }

    public List<Podcast> GetAll() => _repo.GetAll();
    public List<Podcast> GetUnapproved() => _repo.GetUnapproved();
    public void ApprovePodcast(int id) => _repo.Approve(id);
}
