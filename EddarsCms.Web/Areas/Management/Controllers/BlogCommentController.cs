using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Dto.OtherDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers
{
    public class BlogCommentController : Controller
    {
        IBlogCommentService blogCommentServ;
        ILanguageService languageServ;

        public BlogCommentController()
        {
            blogCommentServ = new BlogCommentService();
            languageServ = new LanguageService();
        }


        public ActionResult Index()
        {
            var result = blogCommentServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }

           

            return View(result.Result);
        }

        public ActionResult Edit(int id)
        {
            var allMenus = blogCommentServ.GetAll().Result;
            ViewBag.AllMenus = allMenus;


            var result = blogCommentServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = blogCommentServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ChangeState(int id, bool state)
        {
            var result = blogCommentServ.ChangeState(id, state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetByLangId(int id)
        {
            var result = blogCommentServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}