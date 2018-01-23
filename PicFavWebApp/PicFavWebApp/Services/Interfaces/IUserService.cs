using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;

namespace PicFavWebApp.Services.Interfaces
{
    public interface IUserService
    {
        bool CreateUser(User user);
        User GetUserByPublicId(string id);
        ICollection<User> GetAllUsers();
        string GetUserPublicIdByUsername(string userName);
        string GetSalt(string userName);
        User GetUser(string username, string password);
    }
}