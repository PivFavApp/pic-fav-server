using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PicFavWebApp.Models;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Controllers.Api
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            if (_userService.CreateUser(user))
            {
                return Ok(new {message = "User created"});
            }
            else
            {
                return BadRequest("Very bad request");
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
    }
}