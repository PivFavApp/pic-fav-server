using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models.DTO
{
    public class UserStatisticDTO
    {
        public string UserPublicId { get; set; }
        public string GamePublicId { get; set; }
        public long Date { get; set; }
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