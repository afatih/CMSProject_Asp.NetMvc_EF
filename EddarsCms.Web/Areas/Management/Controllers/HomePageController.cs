using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers
{
    public class HomePageController : Controller
    {
        // GET: Management/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}