using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;
using PicFavWebApp.Services.Interfaces;
using NotImplementedException = System.NotImplementedException;

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

        public User GetUserByPublicId(string id)
        {
            try
            {
                return _userRepository.GetUserByPublicId(id);
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

        public string GetUserPublicIdByUsername(string userName)
        {
            try
            {
                return _userRepository.GetUserPublicIdByUsername(userName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public string GetSalt(string userName)
        {
            try
            {
                return _userRepository.GetSalt(userName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public User GetUser(string username, string password)
        {
            try
            {
                return _userRepository.GetUser(username, password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}