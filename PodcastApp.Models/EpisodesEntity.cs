using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PodcastApp.DTO.Attributes;

namespace PodcastApp.Models
{
    public class Episode
    {
        [Key]
        public int EpisodeId { get; set; }

        [SmartRequired]
        [MaxLength(100)]
        public int PodcastId { get; set; }
        [ForeignKey("PodcastId")]
        public Podcast? Podcast { get; set; }

        [SmartRequired]
        [MaxLength(100)]    
        public string? Title { get; set; }

        [SmartRequired]
        [MaxLength(500)]
        public string? Description { get; set; }

        [SmartRequired]
        [Url]
        public string? AudioUrl { get; set; }

        [SmartRequired]
        public string? Duration { get; set; }

        [SmartRequired]
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;



    }
}
