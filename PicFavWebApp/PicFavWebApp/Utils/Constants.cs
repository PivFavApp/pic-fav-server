using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Utils
{
    public static class Constants
    {
        public const string PATH_TO_GAME_IMAGES = @"~/Content/GameImages/";
        public const string VALID_IMAGE_PREFIX = "Valid_";
        public const string INVALID_IMAGE_PREFIX = "Invalid_";
        public const int VALID_IMAGES_COUNT = 20;
        public const int INVALID_IMAGES_COUNT = 20;
        public const string GET_IMAGE_URL = "api/image?gameName={0}&imageName={1}";

        public const string PUBLISH_BASE_URL = "https://picfavwebapp.azurewebsites.net/";
        //public const string PUBLISH_BASE_URL = "http://localhost:5300/";
    }
}