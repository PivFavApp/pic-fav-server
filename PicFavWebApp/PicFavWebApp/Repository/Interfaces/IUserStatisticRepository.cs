using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;

namespace PicFavWebApp.Repository.Interfaces
{
    public interface IUserStatisticRepository
    {
        void AddUserStatistic(UserStatistic userStat);
        List<UserStatistic> GetAllUserStatistics();
        List<UserStatistic> GetUserStatisticByPublicId(string userPublicId);
    }
}