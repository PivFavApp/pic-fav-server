using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PicFavWebApp.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [Index("IX_GamePublicId", IsUnique = true)]
        [StringLength(450)]
        public string PublicId { get; set; }
        [Required]
        [Index("IX_GameDate", IsUnique = true)]
        public long Date { get; set; }
        [Required]
        [Index("IX_GameName", IsUnique = true)]
        [StringLength(450)]
        public string Name { get; set; }
        public List<GameImage> Images { get; set; }
    }
}