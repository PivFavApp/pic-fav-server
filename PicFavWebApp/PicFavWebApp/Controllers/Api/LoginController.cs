using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Controllers.Api
{
    public class LoginController : ApiController
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult LoginUser(string userName, string password)
        {
            string salt = _userService.GetSalt(userName);

            using (var sha512 = SHA512.Create())
            {
                var hashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(password + salt));

                password = BitConverter.ToString(hashed);
            }

            if (_userService.GetUser(userName, password) != null)
            {
                return Ok("User login successful");
            }

            return BadRequest("Invalid username or password");

        }
    }
}