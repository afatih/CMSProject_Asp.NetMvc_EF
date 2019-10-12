
using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    public class PageController : Controller
    {
        IPageService pageServ;
        public PageController()
        {
            pageServ = new PageService();
        }

        // GET: Page
        public ActionResult Index()
        {
            var pages = pageServ.GetAll();
            if (pages.State!= ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + pages.Message + "')</script>";
            }
            return View(pages.Result);
        }

        public ActionResult Create()
        {
            return View(new PageDto());
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(PageDto pageDto)
        {
            if (!ModelState.IsValid)
            {
                return Json("<script>jsSuccess('İşleminiz başarısız')</script>", JsonRequestBehavior.AllowGet);
            }

            var message = "";
            try
            {
                var result = pageServ.Add(pageDto);

                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    //Bu kısımda işlem başarılıysa reload atılabilir.
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

        public ActionResult Edit(int id)
        {
            var page = pageServ.Get(id);
            if (page.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = page.Message;
            }
            return View(page.Result);
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(PageDto pageDto)
        {
            if (!ModelState.IsValid)
            {
                return Json("<script>jsSuccess('İşleminiz başarısız')</script>", JsonRequestBehavior.AllowGet);
            }

            var message = "";
            try
            {
                var result = pageServ.Update(pageDto);

                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
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
            var result = pageServ.Delete(id);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ChangeState(int id,bool state)
        {
            var result = pageServ.ChangeState(id, state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        


        // yazılacak
        //public JsonResult ChangeState(int id,bool state)
        //{

        //}

    }
}