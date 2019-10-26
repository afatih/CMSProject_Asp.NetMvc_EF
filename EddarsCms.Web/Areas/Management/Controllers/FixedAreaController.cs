using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Common;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers
{
    [SecurityManagement]
    public class FixedAreaController : Controller
    {
        IFixedAreaService fixedAreaServ;

        public FixedAreaController()
        {
            fixedAreaServ = new FixedAreaService();
        }


        public ActionResult Index()
        {
            //Sabit id si 1 olan tr dili için alanlar getirilecek.
            var result = fixedAreaServ.Get(1);
            return View(result.Result);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public JsonResult Index(FixedAreaDto dto)
        {
            var message = "<script>jsError('İşleminiz başarısız')</script>";
            try
            {
                dto.InsanKaynaklariIcerik = Helper.ChangeQuatitionForDb(dto.InsanKaynaklariIcerik);
                var result = fixedAreaServ.AddOrUpdate(dto);
                dto.InsanKaynaklariIcerik = Helper.ChangeQuatitionForEditor(dto.InsanKaynaklariIcerik);
                if (result.State == ProcessStateEnum.Success)
                {
                    message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return Json(message,JsonRequestBehavior.AllowGet);
                }
                else
                {
                    message = "<script>jsError('" + result.Message + "')</script>";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                message = "<script>jsError('" + e.Message + "')</script>";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetFixedAreaById(int id)
        {
            var result = fixedAreaServ.Get(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}