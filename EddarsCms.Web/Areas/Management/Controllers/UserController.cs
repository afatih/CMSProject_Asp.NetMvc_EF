using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers
{
    [SecurityManagement]
    public class UserController : Controller
    {
        IUserService userServ;

        public UserController()
        {
            userServ = new UserService();
        }


        public ActionResult Index()
        {
            var result = userServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }

            return View(result.Result);
        }

        public ActionResult Create()
        {
            return View(new UserDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Create(UserDto dto)
        {
            var message= "<script>jsError('İşleminiz başarısız')</script>";
            if (!ModelState.IsValid)
            {
               
                return Json(message,JsonRequestBehavior.AllowGet);
            }

            if (dto.Password!=dto.Password2)
            {
                message = "<script>jsError('Girilen şifreler uyuşmuyor lütfen kontrol ediniz')</script>";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var result = userServ.Add(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    message = "<script>jsSuccess('" + result.Message + "');$('.form-control').val('')</script>";
                    return Json(message, JsonRequestBehavior.AllowGet);
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

        public ActionResult Edit(int id)
        {


            var result = userServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public JsonResult Edit(UserDto dto, string CurrentImage)
        {
            var message = "<script>jsError('İşleminiz başarısız')</script>";
            if (!ModelState.IsValid)
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            if (dto.Password != dto.Password2)
            {
                message = "<script>jsError('Girilen şifreler uyuşmuyor lütfen kontrol ediniz')</script>";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var result = userServ.Update(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return Json(message, JsonRequestBehavior.AllowGet);
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
        public JsonResult Delete(int id)
        {
            var result = userServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}