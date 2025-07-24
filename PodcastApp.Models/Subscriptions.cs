using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PodcastApp.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int PodcastId { get; set; }
        [ForeignKey("PodcastId")]
        public Podcast? Podcast { get; set; }
    }
}
