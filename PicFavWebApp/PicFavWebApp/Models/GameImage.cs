using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class GameImage
    {
        public int GameImageId { get; set; }

        public string ImageUrl { get; set; }
        public bool IsValid { get; set; }
        public string ImageName { get; set; }

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        [InverseProperty("Images")]
        public Game Game { get; set; }
    }
}