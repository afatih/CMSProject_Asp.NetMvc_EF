using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Dto.OtherDtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers
{
    public class SocialMediaController : Controller
    {
        ISocialMediaService socialMediaServ;


        public SocialMediaController()
        {

            socialMediaServ = new SocialMediaService();
        }


        public ActionResult Index()
        {
            var result = socialMediaServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }

           

            return View(result.Result);
        }

        public ActionResult Create()
        {
            return View(new SocialMediaDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Create(SocialMediaDto dto)
        {
            var message = "<script>jsError('İşleminiz başarısız')</script>";

            if (!ModelState.IsValid)
            {
                return Json(message,JsonRequestBehavior.AllowGet);
            }

            try
            {

                var result = socialMediaServ.Add(dto);
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

        public ActionResult Edit(int id)
        {
            var result = socialMediaServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Edit(SocialMediaDto dto)
        {
            var message = "<script>jsError('İşleminiz başarısız')</script>";
            if (!ModelState.IsValid)
            {
                return Json(message,JsonRequestBehavior.AllowGet);
            }

            try
            {
                var result = socialMediaServ.Update(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return Json(message, JsonRequestBehavior.AllowGet);
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


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = socialMediaServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = socialMediaServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetByLangId(int id)
        {
            var result = socialMediaServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}