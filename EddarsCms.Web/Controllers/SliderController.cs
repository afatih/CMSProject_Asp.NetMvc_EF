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
    public class SliderController : Controller
    {
        ISliderService sliderServ;
        ILanguageService languageServ;

        public SliderController()
        {
            sliderServ = new SliderService();
            languageServ = new LanguageService();
        }



        public ActionResult Index(int languageId = 0)
        {
            var result = sliderServ.GetAll();
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }

            var languages = languageServ.GetAll().Result;
            var selectedLang = languages.First();

            if (languageId != 0)
            {
                selectedLang = languages.Where(x => x.Id == languageId).SingleOrDefault();
            }



            var resultForLang = result.Result.Where(x => x.LanguageId == selectedLang.Id).ToList();

            return View(resultForLang);
        }

        public ActionResult Create()
        {
            return View(new SliderDto());
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(SliderDto dto, HttpPostedFileBase file)
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
                        file.SaveAs(Server.MapPath("~/Images/Sliders/") + pathWidthGuid);
                        dto.Image = pathWidthGuid;
                    }
                }

                var result = sliderServ.Add(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(new SliderDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
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
            var result = sliderServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(SliderDto dto, HttpPostedFileBase file, string CurrentImage)
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
                        file.SaveAs(Server.MapPath("~/Images/Sliders/") + pathWidthGuid);
                        dto.Image = pathWidthGuid;
                    }
                }
                else
                {
                    dto.Image = CurrentImage;
                }

                var result = sliderServ.Update(dto);
                if (result.State == ProcessStateEnum.Success)
                {
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(dto);
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
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
            var result = sliderServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ChangeState(int id, bool state)
        {
            var result = sliderServ.ChangeState(id, state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = sliderServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetByLangId(int id)
        {
            var result = sliderServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}