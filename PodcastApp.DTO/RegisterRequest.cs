﻿namespace PodcastApp.DTO
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }         
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }        
    }
}
