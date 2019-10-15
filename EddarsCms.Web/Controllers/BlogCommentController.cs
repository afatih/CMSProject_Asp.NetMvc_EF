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

namespace EddarsCms.Web.Controllers
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

            var languages = languageServ.GetAll().Result;
            var selectedLang = languages.First();

            var resultForLang = result.Result.Where(x => x.LanguageId == selectedLang.Id).ToList();

            return View(resultForLang);
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


        //[HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        //public ActionResult Edit(BlogCommentDto dto, string CurrentImage)
        //{
        //    var allMenus = blogCommentServ.GetAll().Result;
        //    ViewBag.AllMenus = allMenus;

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
        //        return View(dto);
        //    }

        //    try
        //    {
        //        var result = blogCommentServ.Update(dto);
        //        if (result.State == ProcessStateEnum.Success)
        //        {
        //            ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
        //            return View(dto);
        //        }
        //        else
        //        {
        //            ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
        //            return View(dto);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
        //        return View(dto);
        //    }
        //}


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