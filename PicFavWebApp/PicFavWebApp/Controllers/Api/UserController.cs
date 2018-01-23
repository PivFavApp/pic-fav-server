using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using PicFavWebApp.Models;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Controllers.Api
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            user.PublicId = Guid.NewGuid().ToString();
            user.Salt = user.PublicId;
            // hash pass
            using (var sha512 = SHA512.Create())
            {
                var hashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(user.Password + user.Salt));

                user.Password = BitConverter.ToString(hashed);
            }

            if (_userService.GetUserPublicIdByUsername(user.UserName) == null)
            {
                if (_userService.CreateUser(user))
                {
                    return Ok("User created successfuly");
                }
                else
                {
                    return InternalServerError();
                }
            }
            else
            {
                return BadRequest("User already exists.");
            }
            
        }

        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult GetUserById(string publicId)
        {
            return Ok(_userService.GetUserByPublicId(publicId));
        }
    }
}