using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool CreateUser(User user)
        {
            try
            {
                _userRepository.CreateUser(user);
                return true;
            }
            catch (Exception e)
            {
                // need logger
                Console.WriteLine(e);
                return false;
            }
        }

        public User GerUserByPublicId(Guid id)
        {
            try
            {
                return _userRepository.GerUserByPublicId(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public ICollection<User> GetAllUsers()
        {
            try
            {
                return _userRepository.GetAllUsers();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}