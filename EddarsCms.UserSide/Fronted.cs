using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCrms.UserSide
{
    public static class Fronted
    {
        public static List<SliderDto> BannerList()
        {
            //var currentLangId = 1;
            //var currentLang = LanguageOperation.GetLang();
            //if (currentLang!=null)
            //{
            //    if (currentLangId!=0)
            //    {
            //        currentLangId = currentLang.Id;
            //    }
            //}

            //parametre olarak currentLangId verilmesi daha garanti olabilir ama şimdilik gerek yok buna ilerde patlarsa üstteki gibi olabilir



            List<SliderDto> list = new List<SliderDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Sliders where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            SliderDto slider = new SliderDto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                                Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                                ButtonText = dr.IsNull("ButtonText") ? "" : dr["ButtonText"].ToString(),
                                ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                                ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                                OpenNewTab = dr.IsNull("OpenNewTab") ? false : Convert.ToBoolean(dr["OpenNewTab"]),
                                RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                            };
                            list.Add(slider);
                        }
                    }
                }
            }
            return list;

        }

        public static List<BlogDto> BlogList()
        {
            List<BlogDto> list = new List<BlogDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Blogs where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            BlogDto Blog = new BlogDto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                                BlogBegin = dr.IsNull("BlogBegin") ? "" : dr["BlogBegin"].ToString(),
                                Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                                ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                                ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                                AcceptComment = dr.IsNull("AcceptComment") ? false : Convert.ToBoolean(dr["AcceptComment"]),
                                RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                            };
                            list.Add(Blog);
                        }
                    }
                }
            }
            return list;
        }

        public static BlogDto GetBlog(int id)
        {
            BlogDto blog = new BlogDto();
            using(SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Blogs where Id=@Id", CommandType.Text, new SqlParameter("@Id", id));
                if (dt!=null)
                {
                    if (dt.Rows.Count>0)
                    {
                        DataRow dr = dt.Rows[0];
                        blog = new BlogDto()
                        {
                            Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                            BlogBegin = dr.IsNull("BlogBegin") ? "" : dr["BlogBegin"].ToString(),
                            Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                            ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                            ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                            AcceptComment = dr.IsNull("AcceptComment") ? false : Convert.ToBoolean(dr["AcceptComment"]),
                            RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                            Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                        };
                    }
                }
            }

            return blog;

        }

        public static List<PageDto> PageList()
        {
            List<PageDto> list = new List<PageDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Pages where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            PageDto Page = new PageDto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                                Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                                Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                                ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                                ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                                RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                            };
                            list.Add(Page);
                        }
                    }
                }
            }
            return list;

        }

        public static PageDto GetPage(int id)
        {
            PageDto Page = new PageDto();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Pages where Id=@Id", CommandType.Text, new SqlParameter("@Id", id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Page = new PageDto()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                            Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                            Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                            ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                            ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                            RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                            Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                        };
                    }
                }
            }

            return Page;

        }

        public static List<ProductDto> ProductList()
        {
            List<ProductDto> list = new List<ProductDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Products where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ProductDto Product = new ProductDto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                                Name = dr.IsNull("Name") ? "" : dr["Name"].ToString(),
                                Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                                ImageSmall = dr.IsNull("ImageSmall") ? "" : dr["ImageSmall"].ToString(),
                                ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                                Image3 = dr.IsNull("Image3") ? "" : dr["Image3"].ToString(),

                                RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),

                            };
                            list.Add(Product);
                        }
                    }
                }
            }
            return list;

        }

        public static ProductDto GetProduct(int id)
        {
            ProductDto Product = new ProductDto();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Products where Id=@Id", CommandType.Text, new SqlParameter("@Id", id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Product = new ProductDto()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                            Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                            Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                            ImageSmall = dr.IsNull("ImageSmall") ? "" : dr["ImageSmall"].ToString(),
                            ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                            Image3 = dr.IsNull("Image3") ? "" : dr["Image3"].ToString(),
                             Image4 = dr.IsNull("Image4") ? "" : dr["Image4"].ToString(),
                             MainCatName = dr.IsNull("Image4") ? "" : dr["Image4"].ToString(),
                             MainProdName = dr.IsNull("MainProdName") ? "" : dr["MainProdName"].ToString(),
                             Name = dr.IsNull("Name") ? "" : dr["Name"].ToString(),
                             Video1 = dr.IsNull("Video1") ? "" : dr["Video1"].ToString(),
                             Video2 = dr.IsNull("Video2") ? "" : dr["Video2"].ToString(),
                             Video3 = dr.IsNull("Video3") ? "" : dr["Video3"].ToString(),

                            RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                        };
                    }
                }
            }

            return Product;

        }

        public static List<DutyDto> DutyList()
        {
            List<DutyDto> list = new List<DutyDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Duties where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DutyDto Duty = new DutyDto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                                Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                                Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                                ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                                ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                                RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),

                            };
                            list.Add(Duty);
                        }
                    }
                }
            }
            return list;

        }

        public static List<NewsDto> NewsList()
        {
            List<NewsDto> list = new List<NewsDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from News where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            NewsDto News = new NewsDto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                                Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                                Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                                ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                                ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                                RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),

                            };
                            list.Add(News);
                        }
                    }
                }
            }
            return list;

        }

        

    }
}
