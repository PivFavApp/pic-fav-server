using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public uint Age { get; set; }
        public int Rating { get; set; }
        public string AvatarUrl { get; set; }
        public UserRole Role { get; set; }
    }
}