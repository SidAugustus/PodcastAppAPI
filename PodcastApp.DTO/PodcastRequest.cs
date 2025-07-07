namespace PodcastApp.DTO
{
    public class PodcastRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CreatedByUserId { get; set; }

    }
}
