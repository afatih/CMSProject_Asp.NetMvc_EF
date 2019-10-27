using EddarsCms.Dto.OtherDtos;
using EddarsCms.UserSides;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EddarsCms.Web.Filters
{
    public class InternationalizationAttribute:ActionFilterAttribute
    {
        private  List<LangInfoDto> _ourLanguages;
        private  string _defaultLang;
        private string _currentLang="";



        /// <summary>
        /// Apply locale to current thread
        /// </summary>
        /// <param name="lang">locale name</param>
        private void SetLang(string lang)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(lang);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
        }

        public void SetLanguageToSession(string lang)
        {

            HttpContext.Current.Session["lang"] = _ourLanguages.Where(x => x.Url == lang).SingleOrDefault();
            //System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("lang", lang));

            //System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("langId", _ourLanguages.Where(x=>x.Name==lang).SingleOrDefault().Id.ToString()));

            var z = LanguageOperation.GetLang();


            var y = 8;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                // Languages from our db
                _ourLanguages = LanguageOperation.GetAllLangs();


                // Set default locale
                _defaultLang = _ourLanguages.Where(x => x.Id == 1).SingleOrDefault().Url;

                // Get locale from route values
                string lang = (string)filterContext.RouteData.Values["lang"] ?? _defaultLang;

                SetLanguageToSession(lang);



                // If we haven't found appropriate culture - seet default locale then
                if (_ourLanguages.Where(x => x.Url == lang).Count() == 0)
                {
                    SetLanguageToSession(lang);
                }
            }
            catch (Exception e)
            {

               
            }


        }
    }
}