using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCrms.UserSide
{
    public static class Fronted
    {
        public static List<SliderDto> BannerList(int _langauge = 1)
        {
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select Caption, Description,ButtonText,Url,OpenNewTab");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            return new List<SliderDto>()
                            {
                                new SliderDto()
                                {
                                   ButtonText=item["Caption"].ToString()
                                }
                            };
                         }
                    }
                }
            }
            return new List<SliderDto>();

        }

    }
}
