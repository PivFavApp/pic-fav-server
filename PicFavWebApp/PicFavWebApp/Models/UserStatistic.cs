using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class UserStatistic
    {
        public int UserStatisticId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Statistics")]
        public User User { get; set; }
        public string GamePublicId { get; set; }
        public long Date { get; set; }
        public int Result { get; set; }
    }
}