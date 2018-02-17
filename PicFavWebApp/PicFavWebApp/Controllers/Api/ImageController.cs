using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using PicFavWebApp.Models;
using PicFavWebApp.Services.Interfaces;
using PicFavWebApp.Utils;

namespace PicFavWebApp.Controllers.Api
{
   
    public class ImageController : ApiController
    {
        private readonly IGameService _gameService;

        public ImageController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public HttpResponseMessage GetImage(string gameName, string imageName)
        {
            try
            {
                Game game = _gameService.GetGameByName(gameName);
                //GameImage image = game.Images
                //    .FirstOrDefault(im => im.Game.PublicId == gamePublicId && im.ImageName == imageName);
                
                byte[] file = File.ReadAllBytes(DirectoryUtil.MapPath(Constants.PATH_TO_GAME_IMAGES + game.Name + @"/" + imageName));
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(/*image.ImageBlob*/file)
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                //result.Content.Headers.Add("Access-Control-Allow-Origin", "*");
                return result;

            }
            catch (NullReferenceException e)
            {
                //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                //response.Content = new ObjectContent(typeof(NullReferenceException),e,new JsonMediaTypeFormatter());
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                //response.Content = new ObjectContent(typeof(Exception), e, new JsonMediaTypeFormatter());
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}