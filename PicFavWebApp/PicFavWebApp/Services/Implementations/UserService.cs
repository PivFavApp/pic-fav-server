using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private static Logger logger = LogManager.GetCurrentClassLogger();

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
                logger.Error(e);
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
                logger.Error(e);
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
                logger.Error(e);
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
                logger.Error(e);
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
                logger.Error(e);
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
                logger.Error(e);
                return null;
            }
        }

        public User GetUserWithRawPassword(string username, string password)
        {
            try
            {
                string salt = GetSalt(username);

                using (var sha512 = SHA512.Create())
                {
                    var hashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

                    password = BitConverter.ToString(hashed);
                }

                User user = GetUser(username, password);
                return user;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
            
        }

        public void Dispose()
        {
        }
    }
}