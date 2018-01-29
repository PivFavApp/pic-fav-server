using System.Collections.Generic;
using PicFavWebApp.Models;

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
            var users = new List<User>
            {
                new User{UserId = 1, PublicId = Guid.NewGuid().ToString(), FirstName = "Bohdan333", LastName = "Skarg", Age = (long)22, UserName = "bole.skarg", Password = "bole.skargggg", Role = UserRole.AppUser},
                new User{UserId = 2, PublicId = Guid.NewGuid().ToString(), FirstName = "admin333", LastName = "admin", Age = (long) 100, UserName = "PicFavAdmin", Password = "adminadmin", Role = UserRole.Admin}
            };

            users.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();

            context.Games.AddOrUpdate(new Game { GameId = 1,Date = DateTime.Now.Ticks, Name = "testGame1", Images = new List<GameImage> { new GameImage { GameImageId = 1,IsValid = true, ImageUrl = "testurl" } } });
            context.SaveChanges();
        }
    }
}
