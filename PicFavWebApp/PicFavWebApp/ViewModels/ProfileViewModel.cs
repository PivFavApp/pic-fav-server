using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PicFavWebApp.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username cannot be empty")]
        [RegularExpression(@"^[\S]*$", ErrorMessage = "Username cannot be empty")]
        [Remote("ValidateName", "Account")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}