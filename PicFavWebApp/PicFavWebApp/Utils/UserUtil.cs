using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace PicFavWebApp.Utils
{
    public static class UserUtil
    {
        public static string GetCurrentUser()
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            return identity.Claims.FirstOrDefault(x => x.Type == "publicId")?.Value;
        }
    }
}