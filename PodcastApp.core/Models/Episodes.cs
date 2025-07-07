using System;

namespace PodcastApp.Core.Models
{
    public class Episode
    {
        public int EpisodeId { get; set; }
        public int PodcastId { get; set; }
        public Podcast Podcast { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AudioUrl { get; set; }
        public string Duration { get; set; }
        public DateTime ReleaseDate { get; set; }

       
        
    }
}
