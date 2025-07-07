using PodcastApp.Core.DTO;
using PodcastApp.Core.Models;

namespace PodcastApp.Core.Interface
{
    public interface IPodcastService
    {
        void CreatePodcast(PodcastRequest request);
        List<Podcast> GetAll();
        List<Podcast> GetUnapproved();
        void ApprovePodcast(int podcastId);
    }
}