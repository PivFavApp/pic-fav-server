using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models.DTO
{
    public class GameImageDTO
    {
        public string ImageUrl { get; set; }
        public bool IsValid { get; set; }

        public GameImageDTO(GameImage image)
        {
            ImageUrl = image.ImageUrl;
            IsValid = image.IsValid;
        }
    }
}