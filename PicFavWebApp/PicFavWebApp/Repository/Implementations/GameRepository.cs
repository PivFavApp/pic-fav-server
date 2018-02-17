using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Claims;
using System.Web;
using NLog;
using PicFavWebApp.Models;
using PicFavWebApp.Repository.Interfaces;
using PicFavWebApp.Utils;

namespace PicFavWebApp.Repository.Implementations
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void CreateGame(Game game)
        {
            logger.Debug("Creating new Game");
            context.Games.Add(game);
            context.SaveChanges();
        }

        public void UpdateGame(Game game)
        {
            logger.Debug($"Updating user, publicId : {game.PublicId}");

            //context.Entry(game).State = EntityState.Modified;
            context.Games.AddOrUpdate(game);

            // Load original parent including the child item collection
            var originalParent = context.Games
                .Where(p => p.GameId == game.GameId)
                .Include(p => p.Images)
                .SingleOrDefault();
            // We assume that the parent is still in the DB and don't check for null

            // Update scalar properties of parent,
            // can be omitted if we don't expect changes of the scalar properties
            var parentEntry = context.Entry(originalParent);
            parentEntry.CurrentValues.SetValues(game);

            foreach (var childItem in game.Images)
            {
                var originalChildItem = originalParent.Images
                    .Where(c => c.ImageName == childItem.ImageName && c.GameImageId != 0)
                    .SingleOrDefault();
                // Is original child item with same ID in DB?
                if (originalChildItem != null)
                {
                    // Yes -> Update scalar properties of child item
                    var childEntry = context.Entry(originalChildItem);
                    childEntry.CurrentValues.SetValues(childItem);
                }
                else
                {
                    // No -> It's a new child item -> Insert
                    childItem.GameImageId = 0;
                    originalParent.Images.Add(childItem);
                }
            }

            // Don't consider the child items we have just added above.
            // (We need to make a copy of the list by using .ToList() because
            // _dbContext.ChildItems.Remove in this loop does not only delete
            // from the context but also from the child collection. Without making
            // the copy we would modify the collection we are just interating
            // through - which is forbidden and would lead to an exception.)
            foreach (var originalChildItem in
                         originalParent.Images.Where(c => c.GameImageId != 0).ToList())
            {
                // Are there child items in the DB which are NOT in the
                // new child item collection anymore?
                if (!game.Images.Any(c => c.GameImageId == originalChildItem.GameImageId))
                    // Yes -> It's a deleted child item -> Delete
                    context.GameImages.Remove(originalChildItem);
            }

            context.SaveChanges();
        }

        public void DeleteGame(string publicId)
        {
            Game game = context.Games.Include(x => x.Images).SingleOrDefault(g => g.PublicId == publicId);
            //foreach (var image in game.Images)
            //{
            //    context.GameImages.Remove(image);
            //}
            game.Images.ToList().ForEach(p => context.GameImages.Remove(p));
            context.Entry(game).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public Game GetGameById(string publicId)
        {
            logger.Debug($"Get game by public ID : {publicId}");
            return context.Games.AsNoTracking().Include(x => x.Images).SingleOrDefault(g => g.PublicId == publicId);
        }

        public List<Game> GetAllGames()
        {
            logger.Debug("Get all Games");
            string currentUser = UserUtil.GetCurrentUser();
            List<string> f = context.UserStatistics.Where(x => x.User.PublicId == currentUser)
                .Select(x => x.GamePublicId).ToList();
            return context.Games.Where(x => !f.Contains(x.PublicId)).ToList();
            //return context.Games/*.Include(x => x.Images)*/.ToList();
        }

        public Game GetGameByDate(long date)
        {
            logger.Debug($"Get game by date : {date}");
            return context.Games.Include(x => x.Images).SingleOrDefault(g => g.Date == date);
        }

        public Game GetGameByName(string name)
        {
            logger.Debug($"Get game by name : {name}");
            return context.Games.Include(x => x.Images).SingleOrDefault(g => g.Name == name);
        }
    }
}