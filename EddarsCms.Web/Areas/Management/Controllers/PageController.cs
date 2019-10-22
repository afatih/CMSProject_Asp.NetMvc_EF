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
    public class PageController : Controller
    {
        IPageService pageServ;
        ILanguageService languageServ;
        public PageController()
        {
            pageServ = new PageService();
            languageServ = new LanguageService();
        }

        // GET: Page
        public ActionResult Index()
        {
            var pages = pageServ.GetAll();
            if (pages.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + pages.Message + "')</script>";
            }

            var languages = languageServ.GetAll().Result;
            var selectedLang = languages.First();

            var resultForLang = pages.Result.Where(x => x.LanguageId == selectedLang.Id).ToList();

            return View(resultForLang);
        }

        public ActionResult Create()
        {
            return View(new PageDto());
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PageDto pageDto, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(pageDto);
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
                        file1.SaveAs(Server.MapPath("~/Images/Pages/") + pathWidthGuid);
                        pageDto.ImageCover = pathWidthGuid;
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
                        file2.SaveAs(Server.MapPath("~/Images/Pages/") + pathWidthGuid);
                        pageDto.ImageBig = pathWidthGuid;
                    }
                }

                var result = pageServ.Add(pageDto);
                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(new PageDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
                    return View(pageDto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(pageDto);
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
        public ActionResult Edit(PageDto pageDto, HttpPostedFileBase file1, HttpPostedFileBase file2, string OldCover, string OldBig)
        {
            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(OldCover))
                {
                    pageDto.ImageCover = OldCover;
                }
                if (!string.IsNullOrEmpty(OldBig))
                {
                    pageDto.ImageBig = OldBig;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(pageDto);
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
                        file1.SaveAs(Server.MapPath("~/Images/Pages/") + pathWidthGuid);
                        pageDto.ImageCover = pathWidthGuid;
                    }
                }
                else
                {
                    pageDto.ImageCover = OldCover;
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
                        file2.SaveAs(Server.MapPath("~/Images/Pages/") + pathWidthGuid);
                        pageDto.ImageBig = pathWidthGuid;
                    }
                }
                else
                {
                    pageDto.ImageBig = OldBig;
                }

                var result = pageServ.Update(pageDto);
                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(pageDto);
                }
                else
                {
                    ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
                    return View(pageDto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(pageDto);
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = pageServ.Delete(id);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ChangeState(int id, bool state)
        {
            var result = pageServ.ChangeState(id, state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = pageServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetByLangId(int id)
        {
            var result = pageServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}