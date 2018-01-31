using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PicFavWebApp.Models;
using PicFavWebApp.Models.DTO;
using PicFavWebApp.Services.Interfaces;
using PicFavWebApp.Utils;

namespace PicFavWebApp.Controllers.Api
{
    public class UserStatisticController : ApiController
    {
        private readonly IUserStatisticService _userStatisticService;

        public UserStatisticController(IUserStatisticService userStatisticService)
        {
            _userStatisticService = userStatisticService;
        }

        public IHttpActionResult AddUserStatistic(UserStatisticDTO userStat)
        {
            if (_userStatisticService.AddUserStatistic(userStat))
            {
                return Ok("User statistics added successfuly");
            }

            return BadRequest();
        }

        public IHttpActionResult GetAllUserStatistics()
        {
            List<UserStatisticDTO> statisticsToReturn =
                ObjectConverter.ModelsToDtos<UserStatisticDTO, UserStatistic>(
                    _userStatisticService.GetAllUserStatistics());

            return Ok(statisticsToReturn);
        }

        public IHttpActionResult GetUserStatisticsByUserPublicId(string userPublicId)
        {
            List<UserStatisticDTO> statisticsToReturn =
                ObjectConverter.ModelsToDtos<UserStatisticDTO, UserStatistic>(
                    _userStatisticService.GetUserStatisticByPublicId(userPublicId));
            if (statisticsToReturn != null)
            {
                return Ok(statisticsToReturn);
            }

            return NotFound();
        }
    }
}