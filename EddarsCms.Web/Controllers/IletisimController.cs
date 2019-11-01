using EddarsCms.Dto.BasicDtos;
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
            string message = "<script>$('#errorMessage').html('İşleminiz başarısız')</script>";
            if (result>0)
            {
                message = "<script>$('.medium-input').val('');$('.medium-textarea').val('');$('#errorMessage').html('Mesajınız başarıyla tarafımıza iletilmiştir.');</script>";
            }
            return Json (message, JsonRequestBehavior.AllowGet);
        }
    }
}