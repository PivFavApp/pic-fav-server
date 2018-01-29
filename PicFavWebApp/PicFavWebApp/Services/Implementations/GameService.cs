using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public void Dispose()
        {
        }

        public bool CreateGame(Game game)
        {
            try
            {
                _gameRepository.CreateGame(game);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
        }

        public Game GetGameById(string publicId)
        {
            try
            {
                return _gameRepository.GetGameById(publicId);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
        }

        public List<Game> GetAllGames()
        {
            try
            {
                return _gameRepository.GetAllGames();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
        }

        public Game GetGameByDate(long date)
        {
            try
            {
                return _gameRepository.GetGameByDate(date);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
        }

        public Game GetGameByName(string name)
        {
            try
            {
                return _gameRepository.GetGameByName(name);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
        }

        public bool IsAlreadyExists(Game game)
        {
            return GetGameByName(game.Name) != null || GetGameByDate(game.Date) != null;
        }
    }
}