﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using PicFavWebApp.Models;
using PicFavWebApp.Services.Interfaces;
using PicFavWebApp.Utils;

namespace PicFavWebApp.Controllers.Api
{
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IHttpActionResult CreateGame(Game game)
        {
            if (_gameService.IsAlreadyExists(game))
            {
                return BadRequest("Game already created");
            }
            game.PublicId = Guid.NewGuid().ToString();
            if (_gameService.CreateGame(game))
            {
                return Ok("Game created successfuly");
            }

            return BadRequest();

        }

        public IHttpActionResult GetAllGames()
        {

            return Ok(_gameService.GetAllGames());
        }

        public IHttpActionResult GetGameById(string publicId)
        {
            var game = _gameService.GetGameById(publicId);
            if (game != null)
            {
                return Ok(game);
            }

            return NotFound();
        }

        public IHttpActionResult GetGameByName(string name)
        {
            var game = _gameService.GetGameByName(name);
            if (game != null)
            {
                return Ok(game);
            }

            return NotFound();
        }

        public IHttpActionResult GetGameByDate(long date)
        {
            var game = _gameService.GetGameByDate(date);
            if (game != null)
            {
                return Ok(game);
            }

            return NotFound();
        }
    }
}