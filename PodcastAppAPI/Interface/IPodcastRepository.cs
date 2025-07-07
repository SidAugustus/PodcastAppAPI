namespace PodcastAppAPI.Interface;
using PodcastAppAPI.Models;

public interface IPodcastRepository
{
    void Add(Podcast podcast);
    List<Podcast> GetAll();
    List<Podcast> GetUnapproved();
    void Approve(int podcastId);
}

