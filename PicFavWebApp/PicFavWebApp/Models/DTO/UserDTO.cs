using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models.DTO
{
    public class UserDTO
    {
        public string PublicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public uint Age { get; set; }
        public int Rating { get; set; }
        public string AvatarUrl { get; set; }
        public UserRole Role { get; set; }

        public UserDTO(User user)
        {
            PublicId = user.PublicId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Age = user.Age;
            Rating = user.Rating;
            AvatarUrl = user.AvatarUrl;
            Role = user.Role;
        }
    }
}