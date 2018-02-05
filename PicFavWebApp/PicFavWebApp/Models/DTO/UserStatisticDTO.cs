using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models.DTO
{
    public class UserStatisticDTO
    {
        public string UserPublicId { get; set; }
        [Required]
        public string GamePublicId { get; set; }
        [Required]
        [Range(981284940000, Double.MaxValue)]
        public long Date { get; set; }
        [Required]
        public int Result { get; set; }

        public UserStatisticDTO()
        {

        }

        public UserStatisticDTO(UserStatistic userStatistic)
        {
            UserPublicId = userStatistic.User.PublicId;
            GamePublicId = userStatistic.GamePublicId;
            Date = userStatistic.Date;
            Result = userStatistic.Result;
        }
    }
}