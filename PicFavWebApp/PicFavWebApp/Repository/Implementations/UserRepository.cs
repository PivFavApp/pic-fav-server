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

        public User GerUserByPublicId(Guid id)
        {
            return context.Users.SingleOrDefault(u => u.PublicId == id);
        }

        public ICollection<User> GetAllUsers()
        {
            return context.Users.ToList();
        }
    }
}