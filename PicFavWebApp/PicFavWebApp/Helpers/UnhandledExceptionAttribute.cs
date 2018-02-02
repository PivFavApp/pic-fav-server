using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace PicFavWebApp.Helpers
{
    public class UnhandledExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception.GetType() == typeof(HttpResponseException) )
            {
                var exception = ((HttpResponseException) context.Exception).Response;
                context.Response = new HttpResponseMessage(exception.StatusCode){Content = new StringContent(exception.ReasonPhrase), ReasonPhrase = exception.ReasonPhrase };
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}