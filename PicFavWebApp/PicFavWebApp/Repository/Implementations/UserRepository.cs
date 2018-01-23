using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;

namespace PicFavWebApp.Repository.Implementations
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public void CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetUserByPublicId(string id)
        {
            return context.Users.SingleOrDefault(u => u.PublicId == id);
        }

        public ICollection<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public string GetUserPublicIdByUsername(string userName)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            return user?.PublicId;
        }

        public string GetSalt(string userName)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            return user?.Salt;
        }

        public User GetUser(string username, string password)
        {
            return context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }
    }
}