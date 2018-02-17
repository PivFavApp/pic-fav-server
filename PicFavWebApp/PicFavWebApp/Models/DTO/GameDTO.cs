using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Castle.Core.Internal;

namespace PicFavWebApp.Models.DTO
{
    public class GameDTO
    {
        [Display(Name = "Game public id")]
        public string PublicId { get; set; }
        [Required]
        [Display(Name = "Game date")]
        public long Date { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name = "Game name")]
        public string Name { get; set; }
        public List<GameImageDTO> Images { get; set; }

        public GameDTO()
        {

        }

        public GameDTO(Game game)
        {
            if (game != null)
            {
                PublicId = game.PublicId;
                Date = game.Date;
                Name = game.Name;
                Images = new List<GameImageDTO>();
                if (!game.Images.IsNullOrEmpty())
                {
                    foreach (var image in game.Images)
                    {
                        Images.Add(new GameImageDTO(image));
                    }
                }
            }
            

        }
    }
}