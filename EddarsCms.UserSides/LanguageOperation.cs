using EddarsCms.Dto.OtherDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EddarsCms.UserSides
{
    public class LanguageOperation
    {
        public LangInfoDto Lang { get; set; }

        public static LangInfoDto GetLang()
        {
            LanguageOperation langOp = new LanguageOperation() { Lang = new LangInfoDto() };
            if (HttpContext.Current!=null)
            {
                //var langName = HttpContext.Current.Response.Cookies["lang"];
                //var langId = HttpContext.Current.Response.Cookies["langId"];

                if (HttpContext.Current.Session["lang"] != null)
                    langOp.Lang = (LangInfoDto)HttpContext.Current.Session["lang"];


                //if (langName!=null)
                //{
                //    if (!string.IsNullOrEmpty(langName.Value))
                //    {
                //        langOp.Lang.Name = langName.Value;
                //    }
                  
                //}
                //if (langId!=null)
                //{
                //    if (!string.IsNullOrEmpty(langId.Value))
                //    {
                //        langOp.Lang.Id = Convert.ToInt32(langId.Value);
                //    }
                //}
            }
            return langOp.Lang;
        }


        public static List<LangInfoDto> GetAllLangs()
        {
            List<LangInfoDto> list = new List<LangInfoDto>();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Languages",CommandType.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            var lang = new LangInfoDto()
                            {
                                Id = dr.IsNull("Id") ? 0 : Convert.ToInt32(dr["Id"]),
                                Name = dr.IsNull("Name") ? "" : dr["Name"].ToString(),
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                            };
                            list.Add(lang);

                        }
                    }
                }
            }
            return list;
        }
    }

}
