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
    public class UrunController : Controller
    {
        // GET: Urun
        [Internationalization]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detay(int id, string url)
        {
            var result = Fronted.GetProduct(id);
            return View(result);
        }
    }
}