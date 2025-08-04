using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PodcastApp.DTO.Attributes;

namespace PodcastApp.Models
{
    public class Podcast
    {
        [Key]
        public int PodcastId { get; set; }

        [SmartRequired]
        [MaxLength(100)]
        public string? Title { get; set; }

        [SmartRequired]
        [MaxLength(500)]
        public string? Description { get; set; }

        [SmartRequired]
        [MaxLength(100)]
        public string? Category { get; set; }

        [SmartRequired]
        public bool IsApproved { get; set; }

        [SmartRequired]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedByUserId { get; set; }
        [ForeignKey("CreatedByUserId")]
        public User? CreatedByUser { get; set; }

        [SmartRequired]
        public bool IsFlagged { get; set; } = false;


        public ICollection<Episode>? Episodes { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
    }
}
