using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers.Information
{
    public class NotificationController : Controller
    {
        INotificationService notServ;

        public NotificationController()
        {
            notServ = new NotificationService();
        }


        // GET: Notification
        public ActionResult Index()
        {
            var result = notServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);
        }


        public ActionResult Edit(int id)
        {

            var result = notServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = notServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            var result = notServ.DeleteAll();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetNotifications()
        {
            var result = notServ.GetAll();
            if (result.State==ProcessStateEnum.Success)
            {
                if (result.Result!=null)
                {
                    if (result.Result.Count>0)
                    {
                        result.Result = result.Result.OrderByDescending(x => x.Date).Take(10).ToList();
                    }
                }
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }



    }
}