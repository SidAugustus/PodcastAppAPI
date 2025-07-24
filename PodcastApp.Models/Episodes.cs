using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PodcastApp.Models
{
    public class Episode
    {
        [Key]
        public int EpisodeId { get; set; }

        [Required]
        [MaxLength(100)]
        public int PodcastId { get; set; }
        [ForeignKey("PodcastId")]
        public Podcast? Podcast { get; set; }

        [Required]
        [MaxLength(100)]    
        public string? Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Url]
        public string? AudioUrl { get; set; }

        [Required]
        public string? Duration { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;



    }
}
