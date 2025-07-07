using System;

namespace PodcastApp.Core.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public DateTime SubscribedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PodcastId { get; set; }
        public Podcast Podcast { get; set; }
    }
}
