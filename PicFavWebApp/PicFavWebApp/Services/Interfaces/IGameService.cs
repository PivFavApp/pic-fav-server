using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.Models;

namespace PicFavWebApp.Services.Interfaces
{
    public interface IGameService : IDisposable
    {
        bool CreateGame(Game game);
        Game GetGameById(string publicId);
        List<Game> GetAllGames();
        Game GetGameByDate(long date);
        Game GetGameByName(string name);
        bool IsAlreadyExists(Game game);
    }
}