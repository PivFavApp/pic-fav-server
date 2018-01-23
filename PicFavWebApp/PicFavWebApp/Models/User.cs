using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class User
    {
        // Primary key
        public int UserId { get; set; }

        // Public ID for external usage
        public string PublicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public uint Age { get; set; }
        public int Rating { get; set; }
        public string AvatarUrl { get; set; }
        public UserRole Role { get; set; }

        // Salt for password hashing
        public string Salt { get; set; }
    }
}