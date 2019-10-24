using EddarsCms.Dto.BasicDtos;
using EddarsCms.UserSides;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    public class InsanKaynaklariController : Controller
    {
        // GET: InsanKaynaklari
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Index(HumanResourceDto dto, HttpPostedFileBase file)
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
                        file.SaveAs(Server.MapPath("~/Documents/HumanResource/") + pathWidthGuid);
                        dto.CV = pathWidthGuid;
                    }
                }


                var result = FrontendSenders.SendCv(dto);
                if (result>0)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('İşleminiz başarılı')</script>";
                    return View(new HumanResourceDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                    return View(dto);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(dto);
            }
        }
    }
}