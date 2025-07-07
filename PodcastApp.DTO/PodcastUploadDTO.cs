namespace PodcastApp.DTO
{
    public class PodcastUploadDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
