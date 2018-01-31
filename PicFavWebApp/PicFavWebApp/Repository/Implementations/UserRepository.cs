using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;

namespace PicFavWebApp.Repository.Implementations
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void CreateUser(User user)
        {
            logger.Debug($"Creating new user, publicId : {user.PublicId}");
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetUserByPublicId(string id)
        {
            logger.Debug($"Getting user by publicId : {id}");
            return context.Users.Include(u => u.Statistics).SingleOrDefault(u => u.PublicId == id);
        }

        public ICollection<User> GetAllUsers()
        {
            logger.Debug("Getting all users");
            return context.Users.ToList();
        }

        public string GetUserPublicIdByUsername(string userName)
        {
            logger.Debug("Getting user public id by userName");
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            return user?.PublicId;
        }

        public string GetSalt(string userName)
        {
            logger.Debug("Getting user salt by userName");
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            return user?.Salt;
        }

        public User GetUser(string username, string password)
        {
            logger.Debug("Getting user by username and password");
            return context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }
    }
}