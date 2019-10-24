using EddarsCms.UserSides;
using EddarsCms.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    [Internationalization]
    public class KurumsalController : Controller
    {
        // GET: Kurumsal
        [Internationalization]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detay(int id)
        {
            var page = Fronted.GetPage(id);
            return View(page);
        }

    }
}