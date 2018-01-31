using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;
using PicFavWebApp.Models.DTO;

namespace PicFavWebApp.Services.Interfaces
{
    public interface IUserStatisticService : IDisposable
    {
        bool AddUserStatistic(UserStatistic userStat);
        bool AddUserStatistic(UserStatisticDTO userStat);
        List<UserStatistic> GetAllUserStatistics();
        List<UserStatistic> GetUserStatisticByPublicId(string userPublicId);
    }
}