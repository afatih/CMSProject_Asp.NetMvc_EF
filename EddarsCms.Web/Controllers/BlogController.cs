using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
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

        public BlogController()
        {
            blogServ = new BlogService();
        }

        // GET: Blog
        public ActionResult Index()
        {
            var blogs = blogServ.GetAll();
            if (blogs.State!=ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + blogs.Message + "')</script>";
            }
            return View(blogs.Result);
        }

        public ActionResult Create()
        {
            return View(new BlogDto());
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(BlogDto blogDto, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(blogDto);
            }

            try
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file.FileName);
                        file.SaveAs(Server.MapPath("~/Images/Blogs/") + pathWidthGuid);
                        blogDto.Image = pathWidthGuid;
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
        public ActionResult Edit(BlogDto blogDto, HttpPostedFileBase file,string CurrentImage)
        {
            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(CurrentImage))
                {
                    blogDto.Image = CurrentImage;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(blogDto);
            }

            try
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file.FileName);
                        file.SaveAs(Server.MapPath("~/Images/Blogs/") + pathWidthGuid);
                        blogDto.Image = pathWidthGuid;
                    }
                }
                else
                {
                    blogDto.Image = CurrentImage;
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


    }
}