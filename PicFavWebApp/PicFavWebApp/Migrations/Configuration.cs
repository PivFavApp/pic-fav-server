using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;
using PicFavWebApp.Models;
using PicFavWebApp.Utils;
using UrlHelper = System.Web.Http.Routing.UrlHelper;

namespace PicFavWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PicFavWebApp.DAL.PicFavContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PicFavWebApp.DAL.PicFavContext";
        }

        protected override void Seed(PicFavWebApp.DAL.PicFavContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            try
            {
                
                var users = new List<User>
                {
                    new User{UserId = 1, PublicId = Guid.NewGuid().ToString(), FirstName = "Bohdan333", LastName = "Skarg", Age = (long)22, UserName = "bole.skarg", Password = "bole.skargggg", Role = UserRole.AppUser},
                    new User{UserId = 2, PublicId = Guid.NewGuid().ToString(), FirstName = "admin333", LastName = "admin", Age = (long) 100, UserName = "PicFavAdmin", Password = "adminadmin", Role = UserRole.Admin}
                };
                context.Users.RemoveRange(context.Users.Where(u => u.UserName == "bole.skarg" || u.UserName == "PicFavAdmin"));
                context.SaveChanges();
                users.ForEach(s => context.Users.AddOrUpdate(s));
                context.SaveChanges();

                string gamePublicId = Guid.NewGuid().ToString();
                var gameImages = InitializeTestGameImages("testGame1");
                var games = new List<Game>
                {
                    new Game
                    {
                        GameId = 1,
                        Date = ObjectConverter.GetUnixDate(DateTime.Now),
                        PublicId = gamePublicId,
                        Name = "testGame1",
                        Images = gameImages
                    }
                };
                context.Games.RemoveRange(context.Games.Where(g => g.Name == "testGame1"));
                context.SaveChanges();
                context.Games.AddOrUpdate(games.First());
                context.SaveChanges();
            }
            catch (SqlException e)
            {
                if (!e.Message.Contains("duplicate key"))
                {
                    throw;
                }
            }
        }

        private List<GameImage> InitializeTestGameImages(string gameName)
        {
            try
            {
                List<GameImage> gameImages = new List<GameImage>();
                //string imagesPath = /*HostingEnvironment.ApplicationPhysicalPath*/AppDomain.CurrentDomain.BaseDirectory + Constants.PATH_TO_GAME_IMAGES + @"TestGame/";
                string imagesPath = DirectoryUtil.MapPath(Constants.PATH_TO_GAME_IMAGES + "testGame1");
                bool isImageValid = false;
                string imageName = String.Empty;

                foreach (var image in Directory.GetFiles(imagesPath))
                {
                    imageName = Path.GetFileName(image);
                    gameImages.Add(new GameImage { IsValid = isImageValid, ImageUrl = Constants.PUBLISH_BASE_URL + string.Format(Constants.GET_IMAGE_URL, gameName, imageName), ImageName = imageName });
                    isImageValid = !isImageValid;
                }

                return gameImages;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
