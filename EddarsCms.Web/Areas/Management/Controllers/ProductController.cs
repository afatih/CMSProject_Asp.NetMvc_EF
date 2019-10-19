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
    public class ProductController : Controller
    {
        IProductService productServ;
        ICategoryService categoryServ;
        ILanguageService languageServ;

        public ProductController()
        {
            productServ = new ProductService();
            categoryServ = new CategoryService();
            languageServ = new LanguageService();
        }

        // GET: Product
        public ActionResult Index()
        {
            var Products = productServ.GetAll();
            if (Products.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + Products.Message + "')</script>";
            }

            var languages = languageServ.GetAll().Result;
            var selectedLang = languages.First();

            var resultForLang = Products.Result.Where(x => x.LanguageId == selectedLang.Id).ToList();

            return View(resultForLang);
        }

        public ActionResult Create()
        {
            var allProducts = productServ.GetAll().Result;
            ViewBag.AllProducts = allProducts;
            var allCategories = categoryServ.GetAll().Result;
            ViewBag.AllCategories = allCategories;

            return View(new ProductDto());
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(ProductDto ProductDto, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4)
        {
           


            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(ProductDto);
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
                        file1.SaveAs(Server.MapPath("~/Images/Products/") + pathWidthGuid);
                        ProductDto.ImageBig = pathWidthGuid;
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
                        file2.SaveAs(Server.MapPath("~/Images/Products/") + pathWidthGuid);
                        ProductDto.ImageSmall = pathWidthGuid;
                    }
                }

                if (file3 != null)
                {
                    if (file3.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file3.FileName);
                        file3.SaveAs(Server.MapPath("~/Images/Products/") + pathWidthGuid);
                        ProductDto.Image3 = pathWidthGuid;
                    }
                }

                if (file4 != null)
                {
                    if (file4.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file4.FileName);
                        file4.SaveAs(Server.MapPath("~/Images/Products/") + pathWidthGuid);
                        ProductDto.Image4 = pathWidthGuid;
                    }
                }

                var result = productServ.Add(ProductDto);

                var allProducts = productServ.GetAll().Result;
                ViewBag.AllProducts = allProducts;
                var allCategories = categoryServ.GetAll().Result;
                ViewBag.AllCategories = allCategories;


                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(new ProductDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
                    return View(ProductDto);
                }
            }
            catch (Exception e)
            {
                var allProducts = productServ.GetAll().Result;
                ViewBag.AllProducts = allProducts;
                var allCategories = categoryServ.GetAll().Result;
                ViewBag.AllCategories = allCategories;


                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(ProductDto);
            }
        }

        public ActionResult Edit(int id)
        {
            var allProducts = productServ.GetAll().Result;
            ViewBag.AllProducts = allProducts;
            var allCategories = categoryServ.GetAll().Result;
            ViewBag.AllCategories = allCategories;

            var result = productServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(ProductDto ProductDto, HttpPostedFileBase file1, HttpPostedFileBase file2, HttpPostedFileBase file3, HttpPostedFileBase file4, string OldBig, string OldSmall, string Old3, string Old4)
        {

            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(OldSmall))
                {
                    ProductDto.ImageSmall = OldSmall;
                }
                if (!string.IsNullOrEmpty(OldBig))
                {
                    ProductDto.ImageBig = OldBig;
                }
                if (!string.IsNullOrEmpty(Old3))
                {
                    ProductDto.Image3 = Old3;
                }
                if (!string.IsNullOrEmpty(Old4))
                {
                    ProductDto.Image4= Old4;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(ProductDto);
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
                        file1.SaveAs(Server.MapPath("~/Images/Products/") + pathWidthGuid);
                        ProductDto.ImageBig = pathWidthGuid;
                    }
                }
                else
                {
                    ProductDto.ImageBig = OldBig;
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
                        file2.SaveAs(Server.MapPath("~/Images/Products/") + pathWidthGuid);
                        ProductDto.ImageSmall = pathWidthGuid;
                    }
                }
                else
                {
                    ProductDto.ImageSmall = OldSmall;
                }

                if (file3 != null)
                {
                    if (file3.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file3.FileName);
                        file3.SaveAs(Server.MapPath("~/Images/Products/") + pathWidthGuid);
                        ProductDto.Image3 = pathWidthGuid;
                    }
                }
                else
                {
                    ProductDto.Image3= Old3;
                }

                if (file4 != null)
                {
                    if (file4.ContentLength > 0)
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

                        var pathWidthGuid = guidId + "_" + Path.GetFileName(file4.FileName);
                        file4.SaveAs(Server.MapPath("~/Images/Products/") + pathWidthGuid);
                        ProductDto.Image4 = pathWidthGuid;
                    }
                }
                else
                {
                    ProductDto.Image4 = Old4;
                }

                var result = productServ.Update(ProductDto);

                var allProducts = productServ.GetAll().Result;
                ViewBag.AllProducts = allProducts;
                var allCategories = categoryServ.GetAll().Result;
                ViewBag.AllCategories = allCategories;


                if (result.State == ProcessStateEnum.Success)
                {
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(ProductDto);
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
                    return View(ProductDto);
                }
            }
            catch (Exception e)
            {
                var allProducts = productServ.GetAll().Result;
                ViewBag.AllProducts = allProducts;
                var allCategories = categoryServ.GetAll().Result;
                ViewBag.AllCategories = allCategories;


                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(ProductDto);
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = productServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ChangeState(int id, bool state)
        {
            var result = productServ.ChangeState(id, state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = productServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetByLangId(int id)
        {
            var result = productServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}