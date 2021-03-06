﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using Castle.Core.Internal;
using PicFavWebApp.Models;
using PicFavWebApp.Models.DTO;
using PicFavWebApp.Services.Interfaces;
using PicFavWebApp.Utils;

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
            user.Role = UserRole.AppUser;
            // hash pass
            using (var sha512 = SHA512.Create())
            {
                var hashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(user.Password + user.Salt));

                user.Password = BitConverter.ToString(hashed);
            }

            if (_userService.GetUserPublicIdByUsername(user.UserName) == null && _userService.CreateUser(user))
            {
                return Ok("User created successfuly");   
            }
            else
            {
                return BadRequest("User already exists");
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            if (!users.IsNullOrEmpty())
            {
                return Ok(ObjectConverter.ModelsToDtos<UserDTO,User>(users.ToList()));
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult GetUserById(string publicId)
        {
            UserDTO user = ObjectConverter.ModelToDto<UserDTO, User>(_userService.GetUserByPublicId(publicId));
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();    
        }

        public IHttpActionResult GetUserByUserName(string userName)
        {
            UserDTO user = ObjectConverter.ModelToDto<UserDTO, User>(_userService.GetUserByPublicId(_userService.GetUserPublicIdByUsername(userName)));
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }
    }
}