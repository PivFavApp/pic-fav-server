using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class Statistics
    {
        public int StatisticsId { get; set; }

        public string UserPublicId { get; set; }
        public string GamePublicId { get; set; }
        public long Date { get; set; }
        public int Result { get; set; }
    }
}