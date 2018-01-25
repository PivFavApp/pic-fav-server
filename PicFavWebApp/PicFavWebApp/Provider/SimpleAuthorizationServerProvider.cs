using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc.Async;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Services.Implementations;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            logger.Debug("Processing user token authorization");
            context.Validated(new ClaimsIdentity(context.Options.AuthenticationType));

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            IUserService userService = (IUserService) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
            User user = userService.GetUserWithRawPassword(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }


            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}