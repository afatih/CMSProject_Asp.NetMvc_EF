using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.UserSides;
using EddarsCms.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers
{

    public class HomePageController : Controller
    {
        IUserService userServ;

        public HomePageController()
        {
            userServ = new UserService();
        }


        [SecurityManagement]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserDto dto)
        {
            var user = userServ.GetUserByNamePassword(dto);

            if (user.Result!=null)
            {
                Session["user"] = dto;
            }
            else
            {
                ViewBag.Message = "<script>jsError('Kullanıcı adı veya şifre hatalı')</script>";
                return View();
            }

            return RedirectToAction("Index", "HomePage", new { area = "Management" });
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            return View("Login");
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ForgotPassword(string email)
        {
            var message = "<script>jsError('İşleminiz gerçekleştirilemedi')</script>";

            var user = userServ.Get(email);
            if (user.Result==null)
            {
                message = "<script>jsError('Sisteme kayıtlı böyle bir mail adresi bulunmamaktadır')</script>";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            if (user.Result!=null)
            {
                var result = FrontendSenders.SendPassword(email,"Hesap şifreniz",user.Result.Password);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(message,JsonRequestBehavior.AllowGet);
        }
    }
}