using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PodcastApp.DTO.Attributes;

namespace PodcastApp.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [SmartRequired]
        [MaxLength(100)]
        public string? RoleName { get; set; }

        [SmartRequired]
        [MaxLength(100)]
        public string? Description { get; set; }

        [SmartRequired]
        public bool IsActive { get; set; }

        [SmartRequired]
        public DateTime CreatedAt { get; set; }

        public List<User>? Users { get; set; } 

    }
}
