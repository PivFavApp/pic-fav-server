using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Castle.Core.Internal;
using PicFavWebApp.Models;
using PicFavWebApp.Models.DTO;
using PicFavWebApp.Utils;

namespace PicFavWebApp.ViewModels
{
    public class CreateGameViewModel
    {
        public string PublicId { get; set; }
        [Required]
        [Display(Name = "Game date")]
        public DateTime Date { get; set; }
        [Required]
        [MinLength(3)]
        [Display(Name = "Game name")]
        public string Name { get; set; }
        [Display(Name = "List of valid images")]
        public List<HttpPostedFileBase> ValidImages { get; set; }
        [Display(Name = "List of invalid images")]
        public List<HttpPostedFileBase> InvalidImages { get; set; }

        public List<GameImageDTO> StoredImages { get; set; }

        public CreateGameViewModel()
        {
        }

        public CreateGameViewModel(Game game)
        {
            PublicId = game.PublicId;
            Date = ObjectConverter.GetDateFromUnix(game.Date);
            Name = game.Name;
            StoredImages = new List<GameImageDTO>();
            if (!game.Images.IsNullOrEmpty())
            {
                foreach (var image in game.Images)
                {
                    StoredImages.Add(new GameImageDTO(image));
                }
            }
        }
    }
}