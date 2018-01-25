using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PicFavWebApp.Helpers;

namespace PicFavWebApp.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new UnhandledExceptionAttribute());
            config.Filters.Add(new ValidateModelAttribute());
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});
        }
    }
}