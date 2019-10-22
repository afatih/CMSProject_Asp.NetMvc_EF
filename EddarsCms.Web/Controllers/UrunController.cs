using EddarsCms.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        [Internationalization]
        public ActionResult Index()
        {
            return View();
        }
    }
}