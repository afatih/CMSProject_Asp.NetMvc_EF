using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.UserSides;
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
    [Internationalization]
    public class HomeController : Controller
    {



        // Localize string without any external impact
 
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public JsonResult SendInfoMail(InformationFromUsDto dto)
        {
            var result = FrontendSenders.SendInfoFromUs(dto);
            return Json(result, JsonRequestBehavior.AllowGet);
        }




















        public ActionResult Index2()
        {
            return View();
        }

    }
}