﻿using PodcastApp.Interface;
using PodcastApp.Models;

namespace PodcastApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public List<User> GetFlaggedUsers()
        {
            return _context.Users.Where(u => u.IsFlagged).ToList();
        }

        public List<User> GetSuspendedUsers()
        {
            return _context.Users.Where(u => u.IsSuspended).ToList();
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
