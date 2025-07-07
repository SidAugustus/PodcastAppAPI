namespace PodcastApp.DTO
{
    public class EpisodeDTO
    {
        public int PodcastId { get; set; }      
        public string Title { get; set; }
        public string Description { get; set; }
        public string AudioUrl { get; set; }
        public string Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}

