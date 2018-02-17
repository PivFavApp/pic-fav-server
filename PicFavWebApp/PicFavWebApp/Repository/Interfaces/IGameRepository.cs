using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;

namespace PicFavWebApp.Repository.Interfaces
{
    public interface IGameRepository
    {
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(string publicId);
        Game GetGameById(string publicId);
        List<Game> GetAllGames();
        Game GetGameByDate(long date);
        Game GetGameByName(string name);
    }
}