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
    public class DutyController : Controller
    {
        IDutyService dutyServ;
        ILanguageService languageServ;
        public DutyController()
        {
            dutyServ = new DutyService();
            languageServ = new LanguageService();
        }

        // GET: Duty
        public ActionResult Index()
        {
            var dutys = dutyServ.GetAll();
            if (dutys.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + dutys.Message + "')</script>";
            }

            var languages = languageServ.GetAll().Result;
            var selectedLang = languages.First();

            var resultForLang = dutys.Result.Where(x => x.LanguageId == selectedLang.Id).ToList();

            return View(resultForLang);
        }

        public ActionResult Create()
        {
            return View(new DutyDto());
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DutyDto dutyDto, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(dutyDto);
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
                        file1.SaveAs(Server.MapPath("~/Images/Duties/") + pathWidthGuid);
                        dutyDto.ImageCover = pathWidthGuid;
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
                        file2.SaveAs(Server.MapPath("~/Images/Duties/") + pathWidthGuid);
                        dutyDto.ImageBig = pathWidthGuid;
                    }
                }

                var result = dutyServ.Add(dutyDto);
                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(new DutyDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
                    return View(dutyDto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(dutyDto);
            }
        }

        public ActionResult Edit(int id)
        {
            var duty = dutyServ.Get(id);
            if (duty.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = duty.Message;
            }
            return View(duty.Result);
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DutyDto dutyDto, HttpPostedFileBase file1, HttpPostedFileBase file2, string OldCover, string OldBig)
        {
            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(OldCover))
                {
                    dutyDto.ImageCover = OldCover;
                }
                if (!string.IsNullOrEmpty(OldBig))
                {
                    dutyDto.ImageBig = OldBig;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(dutyDto);
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
                        file1.SaveAs(Server.MapPath("~/Images/Duties/") + pathWidthGuid);
                        dutyDto.ImageCover = pathWidthGuid;
                    }
                }
                else
                {
                    dutyDto.ImageCover = OldCover;
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
                        file2.SaveAs(Server.MapPath("~/Images/Duties/") + pathWidthGuid);
                        dutyDto.ImageBig = pathWidthGuid;
                    }
                }
                else
                {
                    dutyDto.ImageBig = OldBig;
                }

                var result = dutyServ.Update(dutyDto);
                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(dutyDto);
                }
                else
                {
                    ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
                    return View(dutyDto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(dutyDto);
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = dutyServ.Delete(id);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ChangeState(int id, bool state)
        {
            var result = dutyServ.ChangeState(id, state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = dutyServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetByLangId(int id)
        {
            var result = dutyServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}