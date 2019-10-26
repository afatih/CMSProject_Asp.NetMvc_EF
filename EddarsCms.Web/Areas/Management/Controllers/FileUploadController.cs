using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EddarsCms.Web.Areas.Management.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: Management/FileUpload
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Upload(List<HttpPostedFileBase> files)
        {
            return null;
        }


        public class ViewDataUploadFilesResult
        {
            public string name { get; set; }
            public int size { get; set; }
            public string type { get; set; }
            public string url { get; set; }
            public string delete_url { get; set; }
            public string thumbnail_url { get; set; }
            public string delete_type { get; set; }
        }


        public class ImageUploadHandler : IHttpHandler
        {
            public void ProcessRequest(HttpContext context)
            {
                try
                {
                    Dummy dummy = new Dummy();

                    HttpPostedFile filePosted = context.Request.Files["files"];
                    if (filePosted != null && filePosted.ContentLength > 0)
                    {

                    }
                    else
                        context.Response.Write(new JavaScriptSerializer().Serialize(dummy));
                }
                catch
                {
                    context.Response.Write(new JavaScriptSerializer().Serialize(new Dummy()));
                }
            }

            public bool IsReusable
            {
                get
                {
                    return false;
                }
            }
        }


        public class Dummy
        {
            public List<DummyImage> files { get; set; }

            public Dummy()
            {
                this.files = new List<DummyImage>();
            }
        }

        public class DummyImage
        {
            public string deleteType { get; set; }
            public string deleteUrl { get; set; }
            public string name { get; set; }
            public int size { get; set; }
            public string thumbnailUrl { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }
    }
}