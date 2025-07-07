using System;
using System.Collections.Generic;

namespace PodcastAppAPI.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<User> Users { get; set; } 

    }
}
