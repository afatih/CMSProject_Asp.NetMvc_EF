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
    public class InformationFromUsController : Controller
    {
        IInformationFromUsService InformationFromUsServ;

        public InformationFromUsController()
        {
            InformationFromUsServ = new InformationFromUsService();
        }


        public ActionResult Index()
        {
            var result = InformationFromUsServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }



            return View(result.Result);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = InformationFromUsServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}