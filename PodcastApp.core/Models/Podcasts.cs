using System;
using System.Collections.Generic;

namespace PodcastApp.Core.Models
{
    public class Podcast
    {
        public int PodcastId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public ICollection<Episode> Episodes { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
