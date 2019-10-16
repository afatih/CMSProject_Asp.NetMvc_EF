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
    public class HumanResourceController : Controller
    {
        IHumanResourceService HumanResourceServ;

        public HumanResourceController()
        {
            HumanResourceServ = new HumanResourceService();
        }


        public ActionResult Index()
        {
            var result = HumanResourceServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }



            return View(result.Result);
        }

        public ActionResult Edit(int id)
        {
            var allMenus = HumanResourceServ.GetAll().Result;
            ViewBag.AllMenus = allMenus;


            var result = HumanResourceServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = HumanResourceServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        public void DownloadFolder(string fileName)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "filename=" + fileName);
            Response.TransmitFile(Server.MapPath("~/Documents/HumanResource/") + fileName);
            Response.End();
        }
    }
}