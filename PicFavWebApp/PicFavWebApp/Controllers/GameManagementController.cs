using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NLog;
using PicFavWebApp.Helpers;
using PicFavWebApp.Models;
using PicFavWebApp.Models.DTO;
using PicFavWebApp.Services.Interfaces;
using PicFavWebApp.Utils;
using PicFavWebApp.ViewModels;
using XiGp.Server.Web.AdminPortal.Util;

namespace PicFavWebApp.Controllers
{
    [PicFavAuthorize]
    public class GameManagementController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IGameService _gameService;

        public GameManagementController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: GameManagement
        [NoCache]
        public ActionResult GameCreation()
        {
            return View();
        }

        public ActionResult CreateGame()
        {
            string gameName = Request.Form["name"];
            string gameDateString = Request.Form["date"];
            DateTime gameDate;
            if (gameName.IsNullOrWhiteSpace())
            {
                this.PushAlert(AlertType.Danger, "Game name cannot be empty");
                return new HttpStatusCodeResult(400);
            }

            if (gameDateString.IsNullOrWhiteSpace())
            {
                this.PushAlert(AlertType.Danger, "Game date cannot be empty");
                return new HttpStatusCodeResult(400);
            }

            gameDate = Convert.ToDateTime(Request.Form["date"]);

            long longDate = ObjectConverter.GetUnixDate(gameDate);

            if (_gameService.IsAlreadyExists(new Game { Date = longDate, Name = gameName }))
            {
                this.PushAlert(AlertType.Danger, "Game already exists");
                return new HttpStatusCodeResult(400);
                //return RedirectToAction("CreateGame");
            }

            CreateGameViewModel gameViewModel = new CreateGameViewModel
            {
                Date = gameDate,
                Name = gameName,
                InvalidImages = new List<HttpPostedFileBase>(),
                ValidImages = new List<HttpPostedFileBase>()
            };

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                if (file != null && file.FileName.Contains(Constants.VALID_IMAGE_PREFIX))
                {
                    gameViewModel.ValidImages.Add(file);
                }
                else if (file != null && file.FileName.Contains(Constants.INVALID_IMAGE_PREFIX))
                {
                    gameViewModel.InvalidImages.Add(file);
                }
            }

            if (gameViewModel.ValidImages.Count != Constants.VALID_IMAGES_COUNT)
            {
                this.PushAlert(AlertType.Danger, $"Count of valid images should be {Constants.VALID_IMAGES_COUNT} items");
                return new HttpStatusCodeResult(400);
            }
            else if (gameViewModel.InvalidImages.Count != Constants.INVALID_IMAGES_COUNT)
            {
                this.PushAlert(AlertType.Danger, $"Count of invalid images should be {Constants.INVALID_IMAGES_COUNT} items");
                return new HttpStatusCodeResult(400); ;
            }

            Game game;
            if (!UploadFiles(gameViewModel, out game))
            {
                this.PushAlert(AlertType.Danger, "Cannot proceed operation");
                return new HttpStatusCodeResult(500);
            }

            _gameService.CreateGame(game);

            this.PushAlert(AlertType.Success, "Game created successfuly");
            return new HttpStatusCodeResult(200);
        }

        private bool UploadFiles(CreateGameViewModel model, out Game game)
        {
            string pathString = DirectoryUtil.MapPath(Constants.PATH_TO_GAME_IMAGES) + model.Name;
            bool isExists = System.IO.Directory.Exists(pathString);
            if (isExists)
            {
                System.IO.Directory.Delete(pathString, true);
                System.IO.Directory.CreateDirectory(pathString);
            }
            else
            {
                System.IO.Directory.CreateDirectory(pathString);
            }

            game = new Game { Date = ObjectConverter.GetUnixDate(model.Date), Name = model.Name, Images = new List<GameImage>() };

            try
            {
                foreach (HttpPostedFileBase file in model.ValidImages)
                {
                    //Save file content goes here
                    var fName = Guid.NewGuid() + ".jpg";
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = $"{pathString}\\{fName}";
                        file.SaveAs(path);
                    }
                    game.Images.Add(new GameImage { ImageName = fName, IsValid = true, ImageUrl = Constants.PUBLISH_BASE_URL + string.Format(Constants.GET_IMAGE_URL, model.Name, fName) });
                }

                foreach (HttpPostedFileBase file in model.InvalidImages)
                {
                    //Save file content goes here
                    var fName = Guid.NewGuid() + ".jpg";
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = $"{pathString}\\{fName}";
                        file.SaveAs(path);
                    }
                    game.Images.Add(new GameImage { ImageName = fName, IsValid = false, ImageUrl = Constants.PUBLISH_BASE_URL + string.Format(Constants.GET_IMAGE_URL, model.Name, fName) });
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                System.IO.Directory.Delete(pathString, true);
                game = null;
                return false;
            }
        }

        public ActionResult GamesList()
        {
            List<CreateGameViewModel> games = ObjectConverter.ModelsToDtos<CreateGameViewModel, Game>(_gameService.GetAllGames());
            return View(games);
        }

        public ActionResult GameDetails(string publicId)
        {
            Game game = _gameService.GetGameById(publicId);
            CreateGameViewModel model = ObjectConverter.ModelToDto<CreateGameViewModel, Game>(game);
            
            return View(model);
        }

        public ActionResult EditGame()
        {
            string gameName = Request.Form["name"];
            string gameDateString = Request.Form["date"];
            string gamePublicId = Request.Form["publicId"];
            List<string> validDeletedGameIds =
                Request.Form[@Constants.VALID_IMAGE_PREFIX + "DeletedImageUrls"]?.Split(',').ToList();
            List<string> invalidDeletedGameIds =
                Request.Form[@Constants.INVALID_IMAGE_PREFIX + "DeletedImageUrls"]?.Split(',').ToList();
            if (validDeletedGameIds == null)
            {
                validDeletedGameIds = new List<string>();
            }

            if (invalidDeletedGameIds == null)
            {
                invalidDeletedGameIds = new List<string>();
            }

            DateTime gameDate;
            if (gameName.IsNullOrWhiteSpace())
            {
                this.PushAlert(AlertType.Danger, "Game name cannot be empty");
                return new HttpStatusCodeResult(400);
            }

            if (gameDateString.IsNullOrWhiteSpace())
            {
                this.PushAlert(AlertType.Danger, "Game date cannot be empty");
                return new HttpStatusCodeResult(400);
            }

            gameDate = Convert.ToDateTime(Request.Form["date"]);

            CreateGameViewModel gameViewModel = new CreateGameViewModel
            {
                PublicId = gamePublicId,
                Date = gameDate,
                Name = gameName,
                InvalidImages = new List<HttpPostedFileBase>(),
                ValidImages = new List<HttpPostedFileBase>()
            };

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                if (file != null && file.FileName.Contains(Constants.VALID_IMAGE_PREFIX))
                {
                    gameViewModel.ValidImages.Add(file);
                }
                else if (file != null && file.FileName.Contains(Constants.INVALID_IMAGE_PREFIX))
                {
                    gameViewModel.InvalidImages.Add(file);
                }
            }

            var validImgCount = Constants.VALID_IMAGES_COUNT - validDeletedGameIds.Count + gameViewModel.ValidImages.Count;
            var invalidImgCount = Constants.INVALID_IMAGES_COUNT - invalidDeletedGameIds.Count + gameViewModel.InvalidImages.Count;
            if (validImgCount != Constants.VALID_IMAGES_COUNT)
            {
                this.PushAlert(AlertType.Danger, $"Count of valid images should be {Constants.VALID_IMAGES_COUNT} items");
                return new HttpStatusCodeResult(400);
            }
            else if (invalidImgCount != Constants.INVALID_IMAGES_COUNT)
            {
                this.PushAlert(AlertType.Danger, $"Count of invalid images should be {Constants.INVALID_IMAGES_COUNT} items");
                return new HttpStatusCodeResult(400);
            }

            Game game;
            if (!UpdateFiles(gameViewModel, out game, validDeletedGameIds, invalidDeletedGameIds))
            {
                this.PushAlert(AlertType.Danger, "Cannot proceed operation");
                return new HttpStatusCodeResult(500);
            }

            try
            {
                _gameService.UpdateGame(game);
            }
            catch (SqlException e)
            {
                string pathString = DirectoryUtil.MapPath(Constants.PATH_TO_GAME_IMAGES) + gameName;

                Game existingGame = _gameService.GetGameById(gamePublicId);
                string oldPath = DirectoryUtil.MapPath(Constants.PATH_TO_GAME_IMAGES) + existingGame.Name;
                System.IO.Directory.Move(pathString, oldPath);

                if (e.Number == 2601)
                {
                    this.PushAlert(AlertType.Danger, "Game with provided Date or Name already exists.");
                    return new HttpStatusCodeResult(400);
                }
            }

            this.PushAlert(AlertType.Success, "Game updated successfuly");
            return new HttpStatusCodeResult(200);
        }

        private bool UpdateFiles(CreateGameViewModel model, out Game game, List<string> validDeletedFiles, List<string> invalidDeletedFiles)
        {
            string pathString = DirectoryUtil.MapPath(Constants.PATH_TO_GAME_IMAGES) + model.Name;
            Game existingGame = _gameService.GetGameById(model.PublicId);
            if (existingGame.Name != model.Name)
            {
                string oldPath = DirectoryUtil.MapPath(Constants.PATH_TO_GAME_IMAGES) + existingGame.Name;
                System.IO.Directory.Move(oldPath, pathString);
            }

            foreach (var file in validDeletedFiles)
            {
                System.IO.File.Delete(pathString + "\\" + file);
                existingGame.Images.RemoveAll(m => validDeletedFiles.Contains(m.ImageName));
            }

            foreach (var file in invalidDeletedFiles)
            {
                System.IO.File.Delete(pathString + "\\" + file);
                existingGame.Images.RemoveAll(m => invalidDeletedFiles.Contains(m.ImageName));
            }

            //game = new Game { Date = model.Date.Ticks, Name = model.Name, Images = new List<GameImage>() };
            existingGame.Date = ObjectConverter.GetUnixDate(model.Date);
            existingGame.Name = model.Name;
            try
            {
                foreach (HttpPostedFileBase file in model.ValidImages)
                {
                    //Save file content goes here
                    var fName = Guid.NewGuid() + ".jpg";
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = $"{pathString}\\{fName}";
                        file.SaveAs(path);
                    }
                    existingGame.Images.Add(new GameImage { ImageName = fName, IsValid = true, ImageUrl = Constants.PUBLISH_BASE_URL + string.Format(Constants.GET_IMAGE_URL, model.Name, fName) });
                }

                foreach (HttpPostedFileBase file in model.InvalidImages)
                {
                    //Save file content goes here
                    var fName = Guid.NewGuid() + ".jpg";
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = $"{pathString}\\{fName}";
                        file.SaveAs(path);
                    }
                    existingGame.Images.Add(new GameImage { ImageName = fName, IsValid = false, ImageUrl = Constants.PUBLISH_BASE_URL + string.Format(Constants.GET_IMAGE_URL, model.Name, fName) });
                }

                game = existingGame;
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                System.IO.Directory.Delete(pathString, true);
                game = null;
                return false;
            }
        }

        public ActionResult DeleteGame(string publicId)
        {
            _gameService.DeleteGame(publicId);
            return RedirectToAction("GamesList");
        }


    }
}