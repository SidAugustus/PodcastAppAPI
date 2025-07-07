namespace PodcastAppAPI.DTO
{
    public class PodcastRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CreatedByUserId { get; set; } // This information wiill be passed in from the frontend UI by the user
    }

}
