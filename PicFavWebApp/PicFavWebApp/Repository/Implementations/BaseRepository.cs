using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PicFavWebApp.DAL;

namespace PicFavWebApp.Repository.Implementations
{
    public class BaseRepository
    {
        public PicFavContext context = new PicFavContext();
        protected bool Disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}