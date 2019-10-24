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

namespace EddarsCms.Web.Areas.Management.Controllers
{
    public class CoverImageController : Controller
    {
        ICoverImageService coverImageServ;

        public CoverImageController()
        {
            coverImageServ = new CoverImageService();
        }


        public ActionResult Index()
        {
            var result = coverImageServ.Get();
            return View(result.Result);
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Index(CoverImageDto dto, HttpPostedFileBase fileDuty, HttpPostedFileBase fileProduct, HttpPostedFileBase fileBlog, HttpPostedFileBase fileHumanResource, HttpPostedFileBase fileContact, HttpPostedFileBase fileNews, string oldDuty, string oldProduct, string oldBlog, string oldHumanResource, string oldContact, string oldNews)
        {
            try
            {
                #region Duty
                if (fileDuty != null)
                {
                    if (fileDuty.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(fileDuty.FileName);
                        fileDuty.SaveAs(Server.MapPath("~/Images/CoverImages/") + pathWidthGuid);
                        dto.Duty = pathWidthGuid;
                    }
                }
                else
                {
                    dto.Duty = oldDuty;
                }
                #endregion

                #region Product
                if (fileProduct != null)
                {
                    if (fileProduct.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(fileProduct.FileName);
                        fileProduct.SaveAs(Server.MapPath("~/Images/CoverImages/") + pathWidthGuid);
                        dto.Product = pathWidthGuid;
                    }
                }
                else
                {
                    dto.Product = oldProduct;
                }

                #endregion

                #region Blog
                if (fileBlog != null)
                {
                    if (fileBlog.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(fileBlog.FileName);
                        fileBlog.SaveAs(Server.MapPath("~/Images/CoverImages/") + pathWidthGuid);
                        dto.Blog = pathWidthGuid;
                    }
                }
                else
                {
                    dto.Blog = oldBlog;
                }
                #endregion

                #region HumanResource
                if (fileHumanResource != null)
                {
                    if (fileHumanResource.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(fileHumanResource.FileName);
                        fileHumanResource.SaveAs(Server.MapPath("~/Images/CoverImages/") + pathWidthGuid);
                        dto.HumanResource = pathWidthGuid;
                    }
                }
                else
                {
                    dto.HumanResource = oldHumanResource;
                }
                #endregion

                #region Contact
                if (fileContact != null)
                {
                    if (fileContact.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(fileContact.FileName);
                        fileContact.SaveAs(Server.MapPath("~/Images/CoverImages/") + pathWidthGuid);
                        dto.Contact = pathWidthGuid;
                    }
                }
                else
                {
                    dto.Contact = oldContact;
                }
                #endregion

                #region News
                if (fileNews != null)
                {
                    if (fileNews.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(fileNews.FileName);
                        fileNews.SaveAs(Server.MapPath("~/Images/CoverImages/") + pathWidthGuid);
                        dto.News = pathWidthGuid;
                    }
                }
                else
                {
                    dto.News = oldNews;
                }
                #endregion


                var result = coverImageServ.AddOrUpdate(dto);
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
    }
}