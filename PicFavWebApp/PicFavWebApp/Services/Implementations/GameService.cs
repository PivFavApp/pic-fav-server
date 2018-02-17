using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
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
                if (game.PublicId.IsNullOrWhiteSpace())
                {
                    game.PublicId = Guid.NewGuid().ToString();
                }
                _gameRepository.CreateGame(game);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
        }

        public bool UpdateGame(Game game)
        {
            try
            {
                if (game.PublicId.IsNullOrWhiteSpace())
                {
                    game.PublicId = Guid.NewGuid().ToString();
                }
                _gameRepository.UpdateGame(game);
                return true;
            }
            catch (Exception e)
            {
                SqlException innerException = null;
                Exception tmp = e;
                while (innerException == null && tmp != null)
                {
                    if (tmp != null)
                    {
                        innerException = tmp.InnerException as SqlException;
                        tmp = tmp.InnerException;
                    }

                }
                if (innerException != null && innerException.Number == 2601)
                {
                    throw innerException;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteGame(string publicId)
        {
            try
            {
                _gameRepository.DeleteGame(publicId);
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