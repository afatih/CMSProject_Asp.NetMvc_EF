using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    public class BloglarController : Controller
    {
        // GET: Bloglar
        public ActionResult Index()
        {
            return View();
        }
    }
}