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

namespace EddarsCms.Web.Areas.Management.Controllers
{
    public class LanguageController : Controller
    {
        ILanguageService languageServ;

        public LanguageController()
        {
            languageServ = new LanguageService();
        }

        // GET: Language
        public ActionResult Index()
        {
            var languages = languageServ.GetAll();
            if (languages.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = languages.Message;
            }
            return View(languages.Result);
        }

        public ActionResult Create()
        {

            return View(new LanguageDto());
        }

        // POST: Language/Create
        [HttpPost]
        public ActionResult Create(LanguageDto languageDto,HttpPostedFileBase file )
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(languageDto);
            }


            try
            {
                if (file!=null)
                {
                    if (file.ContentLength>0)
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
                        file.SaveAs(Server.MapPath("~/Images/Language/") + pathWidthGuid);
                        languageDto.Image = pathWidthGuid;
                    }
                }


                    var result = languageServ.Add(languageDto);

                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('"+result.Message+"')</script>";
                    return View(new LanguageDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError("+result.Message+")</script>";
                    return View(languageDto);
                }
            }
            catch(Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(languageDto);
            }
        }

        // GET: Language/Edit/5
        public ActionResult Edit(int id)
        {
            var language = languageServ.Get(id);
            if (language.State!= ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + language.Message + "')</script>";
            }
            return View(language.Result);
        }

        // POST: Language/Edit/5
        [HttpPost]
        public ActionResult Edit(LanguageDto languageDto, HttpPostedFileBase file , string CurrentImage)
        {
            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(CurrentImage))
                {
                    languageDto.Image = CurrentImage;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(languageDto);
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
                        file.SaveAs(Server.MapPath("~/Images/Language/") + pathWidthGuid);
                        languageDto.Image = pathWidthGuid;
                    }
                }
                else
                {
                    languageDto.Image = CurrentImage;
                }


                var result = languageServ.Update(languageDto);

                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(languageDto);
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
                    return View(languageDto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(languageDto);
            }
        }

        // POST: Language/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = languageServ.Delete(id);

                return Json(result,JsonRequestBehavior.AllowGet);

        }



        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = languageServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}
