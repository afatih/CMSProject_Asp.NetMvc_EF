using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EddarsCms.Web.Filters
{
    public class SecurityManagement:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["user"] == null)
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    filterContext.Result = new RedirectResult("/Management/HomePage/Login");

            }
        }
    }
}