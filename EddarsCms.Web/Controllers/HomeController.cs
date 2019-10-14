using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(CategoryDto category)
        //{
        //    var result = _cs.Add(category);
        //    ViewBag.ResultState = result.State;
        //    ViewBag.ResultMessage = result.Message;
        //    return View();

        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}