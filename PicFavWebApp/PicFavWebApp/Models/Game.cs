using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class Game
    {
        public int GameId { get; set; }

        public string PublicId { get; set; }
        [Required]
        public long Date { get; set; }
        [Required]
        public string Name { get; set; }
        public List<GameImage> Images { get; set; }
    }
}