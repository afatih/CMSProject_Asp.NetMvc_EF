using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EddarsCms.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
               name: "DefaultLocalizedWithUrl",
               url: "{lang}/{controller}/{action}/{id}/{url}",
               constraints: new { lang = @"(\w{2})|(\w{2}-\w{2})" },   // en or en-US
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, url = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "DefaultLocalized",
                url: "{lang}/{controller}/{action}/{id}",
                constraints: new { lang = @"(\w{2})|(\w{2}-\w{2})" },   // en or en-US
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
              name: "DefaultWithUrl",
              url: "{controller}/{action}/{id}/{url}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, url = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



        //    //localhost8080/hakkimizda girince aşşağıdaki routing dediğimiz gibi istediğimiz aksiyona götürür ama 1 den fazla kontroller ve aksıyon varsa işe yaramaz gibi?
        //    routes.MapRoute(
        //    name: "DefaultWithUrl1",
        //    url: "{url}",
        //    defaults: new { controller = "Kurumsal", action = "Detay", url = UrlParameter.Optional }
        //);


        }
    }
}
