using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Models.DTO;
using PicFavWebApp.Repository.Interfaces;
using PicFavWebApp.Services.Interfaces;

namespace PicFavWebApp.Services.Implementations
{
    public class UserStatisticService : IUserStatisticService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IUserStatisticRepository _userStatisticRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;

        public UserStatisticService(IUserStatisticRepository userStatisticRepository, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _userStatisticRepository = userStatisticRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }

        public void Dispose()
        {
        }

        public bool AddUserStatistic(UserStatistic userStat)
        {
            try
            {
                _userStatisticRepository.AddUserStatistic(userStat);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;

            }
        }

        public bool AddUserStatistic(UserStatisticDTO userStat)
        {
            try
            {
                User user = _userRepository.GetUserByPublicId(userStat.UserPublicId);
                if (user != null && _gameRepository.GetGameById(userStat.GamePublicId) != null)
                {
                    UserStatistic userStatistic = new UserStatistic
                    {
                        Date = userStat.Date,
                        UserId = user.UserId,
                        Result = userStat.Result,
                        GamePublicId = userStat.GamePublicId
                    };
                    user.GeneralRating += userStatistic.Result;
                    user.AverageRating = user.GeneralRating / (user.Statistics.Count + 1);
                    _userRepository.UpdateUser(user);
                    _userStatisticRepository.AddUserStatistic(userStatistic);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
        }

        public List<UserStatistic> GetAllUserStatistics()
        {
            try
            {
                return _userStatisticRepository.GetAllUserStatistics();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
        }

        public List<UserStatistic> GetUserStatisticByPublicId(string userPublicId)
        {
            try
            {
                return _userStatisticRepository.GetUserStatisticByPublicId(userPublicId);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
        }
    }
}