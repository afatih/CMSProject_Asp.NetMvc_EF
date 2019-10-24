using EddarsCms.UserSides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    public class HaberController : Controller
    {
        
        // GET: Haber
        public ActionResult Index()
        {
            var news = Fronted.NewsList();
            return View(news);
        }

        public ActionResult Detay(int id,string url)
        {
            var news = Fronted.GetNews(id);
            return View(news);
        }
    }
}