using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using PicFavWebApp.Models;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Controllers.Api
{
    public class ImageController : ApiController
    {
        private readonly IGameService _gameService;

        public ImageController(IGameService gameService)
        {
            _gameService = gameService;
        }

        private string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
                return HostingEnvironment.MapPath(seedFile);

            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath; //was AbsolutePath but didn't work with spaces according to comments
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }

        public HttpResponseMessage GetImage(string gamePublicId, string imageName)
        {
            try
            {
                Game game = _gameService.GetGameById(gamePublicId);
                GameImage image = game.Images
                    .FirstOrDefault(im => im.Game.PublicId == gamePublicId && im.ImageName == imageName);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(image.ImageBlob)
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                return result;
            }
            catch (NullReferenceException e)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}