using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;

namespace PicFavWebApp.DAL
{
    public class PicFavInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PicFavContext>
    {
        protected override void Seed(PicFavContext context)
        {
            var users = new List<User>
            {
                new User{FirstName = "Bohdan", LastName = "Skarg", Age = 22, UserName = "bole.skarg", Password = "bole.skarg", Role = UserRole.AppUser},
                new User{FirstName = "admin", LastName = "admin", Age = 100, UserName = "PicFavAdmin", Password = "admin", Role = UserRole.Admin}
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            context.Games.Add(new Game {Date = DateTime.Now.Ticks, Name = "testGame1"});
            context.SaveChanges();
        }
    }
}