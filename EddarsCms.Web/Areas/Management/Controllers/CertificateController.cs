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
    public class CertificateController : Controller
    {
        ICertificateService certificateServ;
        ILanguageService languageServ;

        public CertificateController()
        {
            languageServ = new LanguageService();
            certificateServ = new CertificateService();
        }


        public ActionResult Index()
        {
            var result = certificateServ.GetAll();
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
            return View(new CertificateDto());
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(CertificateDto dto, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(dto);
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
                        file.SaveAs(Server.MapPath("~/Images/Certificates/") + pathWidthGuid);
                        dto.Image = pathWidthGuid;
                    }
                }

                var result = certificateServ.Add(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(new CertificateDto());
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
            var result = certificateServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(CertificateDto dto, HttpPostedFileBase file, string CurrentImage)
        {
            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(CurrentImage))
                {
                    dto.Image = CurrentImage;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(dto);
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
                        file.SaveAs(Server.MapPath("~/Images/Certificates/") + pathWidthGuid);
                        dto.Image = pathWidthGuid;
                    }
                }
                else
                {
                    dto.Image = CurrentImage;
                }

                var result = certificateServ.Update(dto);
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
            var result = certificateServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = certificateServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetByLangId(int id)
        {
            var result = certificateServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}