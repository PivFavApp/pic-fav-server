using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;

namespace PicFavWebApp.Repository.Implementations
{
    public class UserStatisticRepository : BaseRepository, IUserStatisticRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void AddUserStatistic(UserStatistic userStat)
        {
            logger.Debug($"Adding user statistic for user ");
            context.UserStatistics.AddOrUpdate(userStat);
            context.SaveChanges();
        }

        public List<UserStatistic> GetAllUserStatistics()
        {
            logger.Debug("Getting all user statistics");
            return context.UserStatistics.Include(st => st.User).ToList();
        }

        public List<UserStatistic> GetUserStatisticByPublicId(string userPublicId)
        {
            logger.Debug($"Getting user statistic for user {userPublicId}");
            return context.UserStatistics.Include(st => st.User).Where(st => st.User.PublicId == userPublicId).ToList();
        }
    }
}