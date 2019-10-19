using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers.Information
{
    public class MailInfoController : Controller
    {
        IMailInfoService mailInfServ;

        public MailInfoController()
        {
            mailInfServ = new MailInfoService();
        }


        public ActionResult Index()
        {
            var result = mailInfServ.Get();
            return View(result.Result);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Index(MailInfoDto dto)
        {
            var message = "<script>jsError('İşleminiz başarısız')</script>";
        

            try
            {
                var result = mailInfServ.AddOrUpdate(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return Json(message,JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "<script>jsError(" + result.Message + ")</script>";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                message = "<script>jsError('" + e.Message + "')</script>";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}