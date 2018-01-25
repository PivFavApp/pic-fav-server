using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using NLog;
using Owin;
using PicFavWebApp.App_Start;
using PicFavWebApp.Provider;
using PicFavWebApp.Services.Interfaces;

[assembly: OwinStartup(typeof(PicFavWebApp.Startup))]
namespace PicFavWebApp
{
    public class Startup
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void Configuration(IAppBuilder app)
        {
            logger.Debug("Configuring OAuth");
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);
            
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // token generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}