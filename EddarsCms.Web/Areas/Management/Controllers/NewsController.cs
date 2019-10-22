using Core.Results;
using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.Dto.OtherDtos;
using EddarsCms.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Areas.Management.Controllers
{
    [SecurityManagement]
    public class NewsController : Controller
    {
        INewsService newsServ;
        ILanguageService languageServ;

        public NewsController()
        {
            languageServ = new LanguageService();
            newsServ = new NewsService();
        }


        public ActionResult Index()
        {
            var result = newsServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }

            var languages = languageServ.GetAll().Result;
            var selectedLang = languages.First();

            var resultForLang = result.Result.Where(x => x.LanguageId == selectedLang.Id).ToList();

            return View(resultForLang);
        }

        public ActionResult Create()
        {
            return View(new NewsDto());
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(NewsDto dto, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(dto);
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
                        file1.SaveAs(Server.MapPath("~/Images/News/") + pathWidthGuid);
                        dto.ImageCover = pathWidthGuid;
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
                        file2.SaveAs(Server.MapPath("~/Images/News/") + pathWidthGuid);
                        dto.ImageBig = pathWidthGuid;
                    }
                }

                var result = newsServ.Add(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(new NewsDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
                    return View(dto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(dto);
            }
        }

        public ActionResult Edit(int id)
        {
            var result = newsServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(NewsDto dto, HttpPostedFileBase file1, HttpPostedFileBase file2, string OldCover, string OldBig)
        {
            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(OldCover))
                {
                    dto.ImageCover = OldCover;
                }
                if (!string.IsNullOrEmpty(OldBig))
                {
                    dto.ImageBig = OldBig;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(dto);
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
                        file1.SaveAs(Server.MapPath("~/Images/News/") + pathWidthGuid);
                        dto.ImageCover = pathWidthGuid;
                    }
                }
                else
                {
                    dto.ImageCover= OldCover;
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
                        file2.SaveAs(Server.MapPath("~/Images/News/") + pathWidthGuid);
                        dto.ImageBig = pathWidthGuid;
                    }
                }
                else
                {
                    dto.ImageBig = OldBig;
                }

                var result = newsServ.Update(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(dto);
                }
                else
                {
                    ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
                    return View(dto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(dto);
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = newsServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = newsServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetByLangId(int id)
        {
            var result = newsServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}