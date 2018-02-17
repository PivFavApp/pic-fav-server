using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PicFavWebApp.Helpers;
using PicFavWebApp.Models;
using PicFavWebApp.Services.Interfaces;
using PicFavWebApp.Utils;
using PicFavWebApp.ViewModels;
using XiGp.Server.Web.AdminPortal.Util;

namespace PicFavWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("GameCreation", "GameManagement");
            }

            return View();
        }

        public ActionResult Login(string username, string password)
        {
            User user = _userService.GetUserWithRawPassword(username, password);
            if (user == null)
            {
                this.PushAlert(AlertType.Danger, "Incorrect username or password");
                return RedirectToAction("Index");
            }

            IdentitySignin(user, user.PublicId, true);
            return RedirectToAction("GameCreation", "GameManagement");
        }

        private void IdentitySignin(User user, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            // create required claims
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName));
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()));
            claims.Add(new Claim("publicId", user.PublicId));

            // custom – my serialized AppUserState object
            //claims.Add(new Claim("userState", appUserState.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        private void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public JsonResult ValidateName(string username)
        {
            var existedId = _userService.GetUserPublicIdByUsername(username);
            var res = string.Format("name {0} is already used", username);
            //if (string.IsNullOrWhiteSpace(Username))
            //{
            //    res = "Username cannot be empty";
            //}
            //else
            if (existedId == null || existedId == UserUtil.GetCurrentUser())
            {
                //in case when we haven't user with that Username or
                //we already have user with the spicifyed Username but Id of this user the same as at edited user - it just updating of existing user.
                res = "true";
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [PicFavAuthorize]
        public ActionResult Profile(ProfileViewModel model)
        {
            return View();
        }

        [PicFavAuthorize]
        public ActionResult UpdateProfile(ProfileViewModel model)
        {
            User user = _userService.GetUserByPublicId(UserUtil.GetCurrentUser());
            using (var sha512 = SHA512.Create())
            {
                var hashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(model.Password + user.Salt));
                user.Password = BitConverter.ToString(hashed);
            }

            user.UserName = model.Username;

            if (_userService.UpdateUser(user))
            {
                this.PushAlert(AlertType.Success, "Your profile was successfuly updated");
            }
            else
            {
                this.PushAlert(AlertType.Danger, "Invalid input data");
            }

            return RedirectToAction("Profile");
        }

        [PicFavAuthorize]
        public ActionResult Logout()
        {
            IdentitySignout();
            return RedirectToAction("Index");
        }
    }
}