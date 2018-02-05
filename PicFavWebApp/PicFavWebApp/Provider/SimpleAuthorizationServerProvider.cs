using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
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
            identity.AddClaim(new Claim("username", context.UserName));
            identity.AddClaim(new Claim("role", user.Role.ToString()));
            identity.AddClaim(new Claim("publicId", user.PublicId));
            identity.AddClaim(new Claim("firstname", user.FirstName));
            identity.AddClaim(new Claim("lastname", user.LastName));
            identity.AddClaim(new Claim("age", user.Age.ToString()));

            var ticket = new AuthenticationTicket(identity, null);

            context.Validated(ticket);
        }
    }
}