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
        public long Age { get; set; }
        public int GeneralRating { get; set; }
        public int AverageRating { get; set; }
        public string AvatarUrl { get; set; }
        public UserRole Role { get; set; }

        public UserDTO(User user)
        {
            PublicId = user.PublicId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Age = user.Age;
            GeneralRating = user.GeneralRating;
            AverageRating = user.AverageRating;
            AvatarUrl = user.AvatarUrl;
            Role = user.Role;
        }
    }
}