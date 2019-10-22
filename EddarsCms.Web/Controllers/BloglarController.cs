using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EddarsCms.Dto.BasicDtos;
using EddarsCms.UserSides;
using EddarsCms.Web.Filters;

namespace EddarsCms.Web.Controllers
{
    public class BloglarController : Controller
    {
        // GET: Bloglar
        [Internationalization]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detay(int id,string url)
        {

            var blog = Fronted.GetBlog(id);

            return View(blog);
        }

        [HttpPost]
        public JsonResult SendComment(BlogCommentDto dto)
        {
            var result = FrontendSenders.SendBlogComment(dto);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}