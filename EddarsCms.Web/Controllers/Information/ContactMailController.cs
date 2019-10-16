using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers.Information
{
    public class ContactMailController : Controller
    {
        IContactMailService contactMailServ;

        public ContactMailController()
        {
            contactMailServ = new ContactMailService();
        }


        public ActionResult Index()
        {
            var result = contactMailServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }



            return View(result.Result);
        }

        public ActionResult Edit(int id)
        {
            var allMenus = contactMailServ.GetAll().Result;
            ViewBag.AllMenus = allMenus;


            var result = contactMailServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = contactMailServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}