using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;

namespace PicFavWebApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GerUserByPublicId(Guid id);
        ICollection<User> GetAllUsers();
    }
}