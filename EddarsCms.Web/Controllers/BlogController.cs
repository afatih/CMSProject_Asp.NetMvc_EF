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

namespace EddarsCms.Web.Controllers
{
    public class BlogController : Controller
    {
        IBlogService blogServ;
        ILanguageService languageServ;

        public BlogController()
        {
            blogServ = new BlogService();
            languageServ = new LanguageService();
        }

        // GET: Blog
        public ActionResult Index()
        {
            var blogs = blogServ.GetAll();
            if (blogs.State!=ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + blogs.Message + "')</script>";
            }

            var languages = languageServ.GetAll().Result;
            var selectedLang = languages.First();

            var resultForLang = blogs.Result.Where(x => x.LanguageId == selectedLang.Id).ToList();

            return View(resultForLang);
        }

        public ActionResult Create()
        {
            return View(new BlogDto());
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(BlogDto blogDto, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(blogDto);
            }

            try
            {
                if (file1 != null)
                {
                    if (file1.ContentLength > 0)
                    {
                        #region random guidId oluşturulduğu kısım
                        var guidId = "";
                        string harfler = "ABCDEFGHIJKLMNOPRSTUVYZ";
                        Random rnd = new Random();
                        for (int i = 0; i <= 3; i++)
                        {
                            var harf = harfler[rnd.Next(harfler.Length)];
                            var sayi = rnd.Next(1, 10);
                            guidId += harf + sayi.ToString();
                        }
                        #endregion

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file1.FileName);
                        file1.SaveAs(Server.MapPath("~/Images/Blogs/") + pathWidthGuid);
                        blogDto.ImageCover = pathWidthGuid;
                    }
                }

                if (file2 != null)
                {
                    if (file2.ContentLength > 0)
                    {
                        #region random guidId oluşturulduğu kısım
                        var guidId = "";
                        string harfler = "ABCDEFGHIJKLMNOPRSTUVYZ";
                        Random rnd = new Random();
                        for (int i = 0; i <= 3; i++)
                        {
                            var harf = harfler[rnd.Next(harfler.Length)];
                            var sayi = rnd.Next(1, 10);
                            guidId += harf + sayi.ToString();
                        }
                        #endregion

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file2.FileName);
                        file2.SaveAs(Server.MapPath("~/Images/Blogs/") + pathWidthGuid);
                        blogDto.ImageBig = pathWidthGuid;
                    }
                }

                var result = blogServ.Add(blogDto);
                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(new BlogDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
                    return View(blogDto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(blogDto);
            }
        }

        public ActionResult Edit(int id)
        {
            var result = blogServ.Get(id);
            if (result.State!=ProcessStateEnum.Success)
            {
                ViewBag.Message= "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(BlogDto blogDto, HttpPostedFileBase file1, HttpPostedFileBase file2, string OldCover, string OldBig)
        {
            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(OldCover))
                {
                    blogDto.ImageCover = OldCover;
                }
                if (!string.IsNullOrEmpty(OldBig))
                {
                    blogDto.ImageBig = OldBig;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(blogDto);
            }

            try
            {
                if (file1 != null)
                {
                    if (file1.ContentLength > 0)
                    {
                        #region random guidId oluşturulduğu kısım
                        var guidId = "";
                        string harfler = "ABCDEFGHIJKLMNOPRSTUVYZ";
                        Random rnd = new Random();
                        for (int i = 0; i <= 3; i++)
                        {
                            var harf = harfler[rnd.Next(harfler.Length)];
                            var sayi = rnd.Next(1, 10);
                            guidId += harf + sayi.ToString();
                        }
                        #endregion

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file1.FileName);
                        file1.SaveAs(Server.MapPath("~/Images/Blogs/") + pathWidthGuid);
                        blogDto.ImageCover = pathWidthGuid;
                    }
                }
                else
                {
                    blogDto.ImageCover = OldCover;
                }

                if (file2 != null)
                {
                    if (file2.ContentLength > 0)
                    {
                        #region random guidId oluşturulduğu kısım
                        var guidId = "";
                        string harfler = "ABCDEFGHIJKLMNOPRSTUVYZ";
                        Random rnd = new Random();
                        for (int i = 0; i <= 3; i++)
                        {
                            var harf = harfler[rnd.Next(harfler.Length)];
                            var sayi = rnd.Next(1, 10);
                            guidId += harf + sayi.ToString();
                        }
                        #endregion

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file2.FileName);
                        file2.SaveAs(Server.MapPath("~/Images/Blogs/") + pathWidthGuid);
                        blogDto.ImageBig = pathWidthGuid;
                    }
                }
                else
                {
                    blogDto.ImageBig = OldBig;
                }

                var result = blogServ.Update(blogDto);
                if (result.State == ProcessStateEnum.Success)
                {
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(blogDto);
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
                    return View(blogDto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(blogDto);
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = blogServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ChangeState(int id, bool state)
        {
            var result = blogServ.ChangeState(id, state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = blogServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetByLangId(int id)
        {
            var result = blogServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}