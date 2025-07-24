using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PodcastApp.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? RoleName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public List<User>? Users { get; set; } 

    }
}
