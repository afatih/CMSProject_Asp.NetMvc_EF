using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Web.Filters;
using EddarsCms.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    public class HomeController : Controller
    {



        // Localize string without any external impact
        [Internationalization]
        public ActionResult Index()
        {
            ViewBag.LangId = HttpContext.Response.Cookies["lang"].Value;


            // Get string from strongly typed localzation resources
            var vm = new FullViewModel { LocalisedString = Strings.SomeLocalizedStrings };
            return View(vm);
        }

        // Get language from query string (by binder)
        public ActionResult LangFromQueryString(string lang)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);

            var vm = new FullViewModel { LocalisedString = Strings.SomeLocalizedStrings };
            return View("Index", vm);
        }


        [Internationalization]
        // Get language as a parameter from route data
        public ActionResult LangFromRouteValues(string lang)
        {
            var vm = new FullViewModel { LocalisedString = Strings.SomeLocalizedStrings };
            return View("Index", vm);
        }


        public ActionResult Index2()
        {
            return View();
        }

    }
}