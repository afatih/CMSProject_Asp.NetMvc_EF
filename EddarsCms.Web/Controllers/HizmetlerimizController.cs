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
    public class HizmetlerimizController : Controller
    {
        // GET: Hizmet

        public ActionResult Index()
        {
            var item = Fronted.DutyList();
            return View(item);
        }

        public ActionResult Detay(int id,string url)
        {
            var duty = Fronted.GetDuty(id);
            return View(duty);
        }
    }
}