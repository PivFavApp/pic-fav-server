using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class User
    {
        // Primary key
        public int UserId { get; set; }

        // Public ID for external usage
        [Index("IX_UserPublicId",IsUnique = true)]
        [StringLength(450)]
        public string PublicId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Username must contain at leasts 8 characters")]
        [Index("IX_UserName", IsUnique = true)]
        [StringLength(450)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must contain at leasts 8 characters")]
        public string Password { get; set; }
        public long Age { get; set; }
        public int Rating { get; set; }
        public string AvatarUrl { get; set; }
        public UserRole Role { get; set; }
        public List<UserStatistic> Statistics { get; set; }

        // Salt for password hashing
        public string Salt { get; set; }
    }
}