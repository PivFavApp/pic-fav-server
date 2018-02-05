using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PicFavWebApp.DAL;
using PicFavWebApp.Helpers;
using PicFavWebApp.Models;

namespace PicFavWebApp.Controllers
{
    [PicFavAuthorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private PicFavContext db = new PicFavContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}