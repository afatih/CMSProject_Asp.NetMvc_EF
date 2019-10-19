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
    public class CategoryController : Controller
    {
        ICategoryService categoryServ;
        ILanguageService languageServ;

        public CategoryController()
        {
            categoryServ = new CategoryService();
            languageServ = new LanguageService();
        }

        // GET: Category
        public ActionResult Index()
        {
            var Categorys = categoryServ.GetAll();
            if (Categorys.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + Categorys.Message + "')</script>";
            }

            var languages = languageServ.GetAll().Result;
            var selectedLang = languages.First();

            var resultForLang = Categorys.Result.Where(x => x.LanguageId == selectedLang.Id).ToList();

            return View(resultForLang);
        }

        public ActionResult Create()
        {
            var allCategories = categoryServ.GetAll().Result;
            ViewBag.AllCategories = allCategories;

            return View(new CategoryDto());
        }

        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Create(CategoryDto CategoryDto, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
           
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(CategoryDto);
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
                        file1.SaveAs(Server.MapPath("~/Images/Categories/") + pathWidthGuid);
                        CategoryDto.ImageBig = pathWidthGuid;
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
                        file2.SaveAs(Server.MapPath("~/Images/Categories/") + pathWidthGuid);
                        CategoryDto.ImageSmall = pathWidthGuid;
                    }
                }

                var result = categoryServ.Add(CategoryDto);

                var allCategories = categoryServ.GetAll().Result;
                ViewBag.AllCategories = allCategories;

                if (result.State == ProcessStateEnum.Success)
                {
                    //ViewBag.Message = result.Message;
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(new CategoryDto());
                }
                else
                {
                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
                    return View(CategoryDto);
                }
            }
            catch (Exception e)
            {
                var allCategories = categoryServ.GetAll().Result;
                ViewBag.AllCategories = allCategories;

                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(CategoryDto);
            }
        }

        public ActionResult Edit(int id)
        {
            var allCategories = categoryServ.GetAll().Result;
            ViewBag.AllCategories = allCategories;

            var result = categoryServ.Get(id);
            if (result.State != ProcessStateEnum.Success)
            {
                ViewBag.Message = "<script>jsError('" + result.Message + "')</script>";
            }
            return View(result.Result);

        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(CategoryDto CategoryDto, HttpPostedFileBase file1, HttpPostedFileBase file2, string OldBig, string OldSmall)
        {
          

            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(OldSmall))
                {
                    CategoryDto.ImageSmall = OldSmall;
                }
                if (!string.IsNullOrEmpty(OldBig))
                {
                    CategoryDto.ImageBig = OldBig;
                }
                ViewBag.Message = "<script>jsError('İşleminiz başarısız')</script>";
                return View(CategoryDto);
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
                        file1.SaveAs(Server.MapPath("~/Images/Categories/") + pathWidthGuid);
                        CategoryDto.ImageBig = pathWidthGuid;
                    }
                }
                else
                {
                    CategoryDto.ImageBig = OldBig;
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
                        file2.SaveAs(Server.MapPath("~/Images/Categories/") + pathWidthGuid);
                        CategoryDto.ImageSmall = pathWidthGuid;
                    }
                }
                else
                {
                    CategoryDto.ImageSmall = OldSmall;
                }

                var result = categoryServ.Update(CategoryDto);

                var allCategories = categoryServ.GetAll().Result;
                ViewBag.AllCategories = allCategories;


                if (result.State == ProcessStateEnum.Success)
                {
                    ViewBag.Message = "<script>jsSuccess('" + result.Message + "')</script>";
                    return View(CategoryDto);
                }
                else
                {
                    

                    ViewBag.Message = "<script>jsError(" + result.Message + ")</script>";
                    return View(CategoryDto);
                }
            }
            catch (Exception e)
            {
                var allCategories = categoryServ.GetAll().Result;
                ViewBag.AllCategories = allCategories;


                ViewBag.Message = "<script>jsError('" + e.Message + "')</script>";
                return View(CategoryDto);
            }
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = categoryServ.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult ChangeState(int id, bool state)
        {
            var result = categoryServ.ChangeState(id, state);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Reorder(List<ReorderDto> list)
        {

            var result = categoryServ.Reorder(list);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetByLangId(int id)
        {
            var result = categoryServ.GetByLangId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}