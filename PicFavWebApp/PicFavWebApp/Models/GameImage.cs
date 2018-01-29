using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class GameImage
    {
        public int GameImageId { get; set; }

        public string ImageUrl { get; set; }
        public bool IsValid { get; set; }
    }
}