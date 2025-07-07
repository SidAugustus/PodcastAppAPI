using PodcastAppAPI.DTO;
using PodcastAppAPI.Models;

public interface IPodcastService
{
    void CreatePodcast(PodcastRequest request);
    List<Podcast> GetAll();
    List<Podcast> GetUnapproved();
    void ApprovePodcast(int podcastId);
}
