using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;

namespace PicFavWebApp.Repository.Implementations
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void CreateGame(Game game)
        {
            logger.Debug("Creating new Game");
            context.Games.Add(game);
            context.SaveChanges();
        }

        public Game GetGameById(string publicId)
        {
            logger.Debug($"Get game by public ID : {publicId}");
            return context.Games.SingleOrDefault(g => g.PublicId == publicId);
        }

        public List<Game> GetAllGames()
        {
            logger.Debug("Get all Games");
            return context.Games.ToList();
        }

        public Game GetGameByDate(long date)
        {
            logger.Debug($"Get game by date : {date}");
            return context.Games.SingleOrDefault(g => g.Date == date);
        }

        public Game GetGameByName(string name)
        {
            logger.Debug($"Get game by name : {name}");
            return context.Games.SingleOrDefault(g => g.Name == name);
        }
    }
}