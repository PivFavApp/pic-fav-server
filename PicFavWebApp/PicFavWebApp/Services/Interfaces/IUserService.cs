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
        User GerUserByPublicId(Guid id);
        ICollection<User> GetAllUsers();
    }
}