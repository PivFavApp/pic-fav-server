using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Castle.Core.Internal;
using Microsoft.AspNet.Identity;
using PicFavWebApp.Models;
using PicFavWebApp.Models.DTO;
using PicFavWebApp.Services.Interfaces;
using PicFavWebApp.Utils;

namespace PicFavWebApp.Controllers.Api
{
    [Authorize]
    public class UserStatisticController : ApiController
    {
        private readonly IUserStatisticService _userStatisticService;
        private readonly IGameService _gameService;
        private readonly IUserService _userService;

        public UserStatisticController(IUserStatisticService userStatisticService,
            IGameService gameService, IUserService userService)
        {
            _userStatisticService = userStatisticService;
            _gameService = gameService;
            _userService = userService;
        }

        public IHttpActionResult AddUserStatistic(UserStatisticDTO userStat)
        {
            if (userStat.UserPublicId.IsNullOrEmpty())
            {
                userStat.UserPublicId = UserUtil.GetCurrentUser();
            }

            if (_userStatisticService.AddUserStatistic(userStat))
            {
                return Ok("User statistics added successfuly");
            }

            return BadRequest();
        }

        public IHttpActionResult GetUserStatistics(bool allUsers)
        {
            if (allUsers)
            {
                List<UserStatisticDTO> statisticsToReturn =
                    ObjectConverter.ModelsToDtos<UserStatisticDTO, UserStatistic>(
                        _userStatisticService.GetAllUserStatistics());

                return Ok(statisticsToReturn);
            }

            return NotFound();
        }

        public IHttpActionResult GetUserStatistics()
        {
            List<UserStatisticDTO> statisticsToReturn =
                ObjectConverter.ModelsToDtos<UserStatisticDTO, UserStatistic>(
                    _userStatisticService.GetUserStatisticByPublicId(UserUtil.GetCurrentUser()));
            if (statisticsToReturn != null)
            {
                return Ok(statisticsToReturn);
            }

            return NotFound();
        }
    }
}