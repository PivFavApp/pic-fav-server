using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        private readonly IGameRepository _gameRepository;

        public UserService(IUserRepository userRepository, IGameRepository gameRepository)
        {
            _userRepository = userRepository;
            _gameRepository = gameRepository;
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

        public User GetUserWithRawPassword(string username, string password)
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

        public void Dispose()
        {
        }
    }
}