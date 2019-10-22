using EddarsCms.Dto.BasicDtos;
using EddarsCms.UserSides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    public class IletisimController : Controller
    {
        // GET: Iletisim
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SendMail(ContactMailDto dto)
        {
            var result = FrontendSenders.SendMail(dto);
            return Json (result,JsonRequestBehavior.AllowGet);
        }
    }
}