using EddarsCms.Dto.BasicDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.UserSides
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
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                                CreatedDate = dr.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(dr["CreatedDate"])
                            };
                            list.Add(Blog);
                        }
                    }
                }
            }
            return list;
        }


        //rasgele 3 bloğu çeker
        public static List<BlogDto> RelatedBlogList()
        {
            List<BlogDto> list = new List<BlogDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select top 3 * from Blogs where State=1 and LanguageId=@LanguageId order by NEWID() ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
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
                                ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                                CreatedDate = dr.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(dr["CreatedDate"]),
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
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Blogs where Id=@Id", CommandType.Text, new SqlParameter("@Id", id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        blog = new BlogDto()
                        {
                            Id = dr.IsNull("Id") ? 0 : Convert.ToInt32(dr["Id"]),
                            Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                            BlogBegin = dr.IsNull("BlogBegin") ? "" : dr["BlogBegin"].ToString(),
                            Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                            ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                            ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                            AcceptComment = dr.IsNull("AcceptComment") ? false : Convert.ToBoolean(dr["AcceptComment"]),
                            RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                            Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                            SeoTitle = dr.IsNull("SeoTitle") ? "" : dr["SeoTitle"].ToString(),
                            SeoDescription = dr.IsNull("SeoDescription") ? "" : dr["SeoDescription"].ToString(),
                            CreatedDate = dr.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(dr["CreatedDate"])
                        };
                    }
                }
            }

            return blog;

        }

        public static List<BlogCommentDto> BlogCommentList(int blogId)
        {
            List<BlogCommentDto> list = new List<BlogCommentDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from BlogComments where State=1 and BlogId=@BlogId order by RowNumber ", CommandType.Text, new SqlParameter("@BlogId", blogId));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            BlogCommentDto BlogComment = new BlogCommentDto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Comment = dr.IsNull("Comment") ? "" : dr["Comment"].ToString(),
                                Date = dr.IsNull("Date") ? DateTime.Now : Convert.ToDateTime(dr["Date"]),
                                UserName = dr.IsNull("UserName") ? "" : dr["UserName"].ToString()
                            };
                            list.Add(BlogComment);
                        }
                    }
                }
            }
            return list;
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
                            Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                            SeoDescription = dr.IsNull("SeoDescription") ? "" : dr["SeoDescription"].ToString(),
                            SeoTitle = dr.IsNull("SeoTitle") ? "" : dr["SeoTitle"].ToString(),
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
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),

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
                            ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                            ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                            ImageSmall = dr.IsNull("ImageSmall") ? "" : dr["ImageSmall"].ToString(),
                            Image3 = dr.IsNull("Image3") ? "" : dr["Image3"].ToString(),
                            Image4 = dr.IsNull("Image4") ? "" : dr["Image4"].ToString(),
                            Name = dr.IsNull("Name") ? "" : dr["Name"].ToString(),
                            Video1 = dr.IsNull("Video1") ? "" : dr["Video1"].ToString(),
                            Video2 = dr.IsNull("Video2") ? "" : dr["Video2"].ToString(),
                            Video3 = dr.IsNull("Video3") ? "" : dr["Video3"].ToString(),
                            Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                            SeoTitle = dr.IsNull("SeoTitle") ? "" : dr["SeoTitle"].ToString(),
                            SeoDescription = dr.IsNull("SeoDescription") ? "" : dr["SeoDescription"].ToString(),

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

        public static DutyDto GetDuty(int id)
        {
            DutyDto Duty = new DutyDto();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Duties where Id=@Id", CommandType.Text, new SqlParameter("@Id", id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Duty = new DutyDto()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                            Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                            Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                            ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                            ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                            Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                            RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                            SeoTitle = dr.IsNull("SeoTitle") ? "" : dr["SeoTitle"].ToString(),
                            SeoDescription = dr.IsNull("SeoDescription") ? "" : dr["SeoDescription"].ToString(),
 
                        };
                    }
                }
            }

            return Duty;

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
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                                CreatedDate = dr.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(dr["CreatedDate"]),

                            };
                            list.Add(News);
                        }
                    }
                }
            }
            return list;

        }

        public static NewsDto GetNews(int id)
        {
            NewsDto News = new NewsDto();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from News where Id=@Id", CommandType.Text, new SqlParameter("@Id", id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        News = new NewsDto()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                            Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                            Content = dr.IsNull("Content") ? "" : dr["Content"].ToString(),
                            ImageCover = dr.IsNull("ImageCover") ? "" : dr["ImageCover"].ToString(),
                            ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                            RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),
                            Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                            SeoTitle = dr.IsNull("SeoTitle") ? "" : dr["SeoTitle"].ToString(),
                            SeoDescription = dr.IsNull("SeoDescription") ? "" : dr["SeoDescription"].ToString(),
                            CreatedDate = dr.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(dr["CreatedDate"]),

                        };
                    }
                }
            }

            return News;

        }

        public static List<SocialMediaDto> SocialMediaList()
        {
            List<SocialMediaDto> list = new List<SocialMediaDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from SocialMedias where State=1 order by RowNumber ", CommandType.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            SocialMediaDto SocialMedia = new SocialMediaDto()
                            {

                                Icon = dr.IsNull("Icon") ? "" : dr["Icon"].ToString(),
                                Name = dr.IsNull("Name") ? "" : dr["Name"].ToString(),
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                                RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),

                            };
                            list.Add(SocialMedia);
                        }
                    }
                }
            }
            return list.ToList();

        }

        public static ContactInfoDto GetContactInfo()
        {
            ContactInfoDto ContactInfo = new ContactInfoDto();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from ContactInfoes", CommandType.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ContactInfo = new ContactInfoDto()
                        {
                            Adress = dr.IsNull("Adress") ? "" : dr["Adress"].ToString(),
                            EMail = dr.IsNull("EMail") ? "" : dr["EMail"].ToString(),
                            Fax = dr.IsNull("Fax") ? "" : dr["Fax"].ToString(),
                            Image = dr.IsNull("Image") ? "" : dr["Image"].ToString(),
                            Name = dr.IsNull("Name") ? "" : dr["Name"].ToString(),
                            Phone1 = dr.IsNull("Phone1") ? "" : dr["Phone1"].ToString(),
                            Phone2 = dr.IsNull("Phone2") ? "" : dr["Phone2"].ToString(),
                            MapLocation = dr.IsNull("MapLocation") ? "" : dr["MapLocation"].ToString(),
                            Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),

                            SeoTitle = dr.IsNull("SeoTitle") ? "" : dr["SeoTitle"].ToString(),
                            SeoDescription = dr.IsNull("SeoDescription") ? "" : dr["SeoDescription"].ToString(),
                              
                        };
                    }
                }
            }

            return ContactInfo;

        }

        public static FixedAreaDto GetFixedArea()
        {
            FixedAreaDto FixedArea = new FixedAreaDto();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from FixedAreas where LanguageId=@LanguageId", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        FixedArea = new FixedAreaDto()
                        {

                            IletisimFormBaslik = dr.IsNull("IletisimFormBaslik") ? "" : dr["IletisimFormBaslik"].ToString(),
                            IletisimGonder = dr.IsNull("IletisimGonder") ? "" : dr["IletisimGonder"].ToString(),
                            IletisimIsim = dr.IsNull("IletisimIsim") ? "" : dr["IletisimIsim"].ToString(),
                            IletisimKonu = dr.IsNull("IletisimKonu") ? "" : dr["IletisimKonu"].ToString(),
                            IletisimEMail = dr.IsNull("IletisimEMail") ? "" : dr["IletisimEMail"].ToString(),
                            IletisimTelefon = dr.IsNull("IletisimTelefon") ? "" : dr["IletisimTelefon"].ToString(),
                            IletisimMesaj = dr.IsNull("IletisimMesaj") ? "" : dr["IletisimMesaj"].ToString(),
                            IletisimBilgiBaslik = dr.IsNull("IletisimBilgiBaslik") ? "" : dr["IletisimBilgiBaslik"].ToString(),
                            IletisimAdresBaslik = dr.IsNull("IletisimAdresBaslik") ? "" : dr["IletisimAdresBaslik"].ToString(),
                            IletisimEMailBaslik = dr.IsNull("IletisimEMailBaslik") ? "" : dr["IletisimEMailBaslik"].ToString(),


                            AnaSayfa = dr.IsNull("AnaSayfa") ? "" : dr["AnaSayfa"].ToString(),
                            AnaSayfaKurumsal = dr.IsNull("AnaSayfaKurumsal") ? "" : dr["AnaSayfaKurumsal"].ToString(),
                            AnaSayfaHizmetlerimiz = dr.IsNull("AnaSayfaHizmetlerimiz") ? "" : dr["AnaSayfaHizmetlerimiz"].ToString(),
                            AnaSayfaUrunlerimiz = dr.IsNull("AnaSayfaUrunlerimiz") ? "" : dr["AnaSayfaUrunlerimiz"].ToString(),
                            AnaSayfaBlog = dr.IsNull("AnaSayfaBlog") ? "" : dr["AnaSayfaBlog"].ToString(),
                            AnaSayfaIletisim = dr.IsNull("AnaSayfaIletisim") ? "" : dr["AnaSayfaIletisim"].ToString(),
                            AnaSayfaHaber = dr.IsNull("AnaSayfaHaber") ? "" : dr["AnaSayfaHaber"].ToString(),
                            AnaSayfaUrunlerBaslik = dr.IsNull("AnaSayfaUrunlerBaslik") ? "" : dr["AnaSayfaUrunlerBaslik"].ToString(),
                            AnaSayfaBlogBaslik = dr.IsNull("AnaSayfaBlogBaslik") ? "" : dr["AnaSayfaBlogBaslik"].ToString(),
                            AnaSayfaInsanKaynaklari = dr.IsNull("AnaSayfaInsanKaynaklari") ? "" : dr["AnaSayfaInsanKaynaklari"].ToString(),
                            AnaSayfaHaberOku = dr.IsNull("AnaSayfaHaberOku") ? "" : dr["AnaSayfaHaberOku"].ToString(),

                            FooterEnSonBloglar = dr.IsNull("FooterEnSonBloglar") ? "" : dr["FooterEnSonBloglar"].ToString(),
                            FooterHaberdarOlAciklama = dr.IsNull("FooterHaberdarOlAciklama") ? "" : dr["FooterHaberdarOlAciklama"].ToString(),
                             

                            InsanKaynaklariBaslik = dr.IsNull("InsanKaynaklariBaslik") ? "" : dr["InsanKaynaklariBaslik"].ToString(),
                            InsanKaynaklariAd = dr.IsNull("InsanKaynaklariAd") ? "" : dr["InsanKaynaklariAd"].ToString(),
                            InsanKaynaklariDosyaSec = dr.IsNull("InsanKaynaklariDosyaSec") ? "" : dr["InsanKaynaklariDosyaSec"].ToString(),
                            InsanKaynaklariGonder = dr.IsNull("InsanKaynaklariGonder") ? "" : dr["InsanKaynaklariGonder"].ToString(),
                            InsanKaynaklariIcerik = dr.IsNull("InsanKaynaklariIcerik") ? "" : dr["InsanKaynaklariIcerik"].ToString(),
                            InsanKaynaklariMail = dr.IsNull("InsanKaynaklariMail") ? "" : dr["InsanKaynaklariMail"].ToString(),
                            InsanKaynaklariMesaj = dr.IsNull("InsanKaynaklariMesaj") ? "" : dr["InsanKaynaklariMesaj"].ToString(),
                            InsanKaynaklariSoyad = dr.IsNull("InsanKaynaklariSoyad") ? "" : dr["InsanKaynaklariSoyad"].ToString(),
                            InsanKaynaklariTelefon = dr.IsNull("InsanKaynaklariTelefon") ? "" : dr["InsanKaynaklariTelefon"].ToString(),



                            BlogDetayIliskiliPost = dr.IsNull("BlogDetayIliskiliPost") ? "" : dr["BlogDetayIliskiliPost"].ToString(),
                            BlogDetayYorumSayi = dr.IsNull("BlogDetayYorumSayi") ? "" : dr["BlogDetayYorumSayi"].ToString(),
                            BlogDetayYorumBaslik = dr.IsNull("BlogDetayYorumBaslik") ? "" : dr["BlogDetayYorumBaslik"].ToString(),
                            BlogDetayIsim = dr.IsNull("BlogDetayIsim") ? "" : dr["BlogDetayIsim"].ToString(),
                            BlogDetayEmail = dr.IsNull("BlogDetayEmail") ? "" : dr["BlogDetayEmail"].ToString(),
                            BlogDetayWebSite = dr.IsNull("BlogDetayWebSite") ? "" : dr["BlogDetayWebSite"].ToString(),
                            BlogDetayYorumYaz = dr.IsNull("BlogDetayYorumYaz") ? "" : dr["BlogDetayYorumYaz"].ToString(),
                            BlogDetayGonder = dr.IsNull("BlogDetayGonder") ? "" : dr["BlogDetayGonder"].ToString(),



                            GenelBiziTakipEt = dr.IsNull("GenelBiziTakipEt") ? "" : dr["GenelBiziTakipEt"].ToString(),
                            GenelBizdenHaberdarOl = dr.IsNull("GenelBizdenHaberdarOl") ? "" : dr["GenelBizdenHaberdarOl"].ToString(),
                            GenelEpostaGir = dr.IsNull("GenelEpostaGir") ? "" : dr["GenelEpostaGir"].ToString(),
                            GenelInstagram = dr.IsNull("GenelInstagram") ? "" : dr["GenelInstagram"].ToString(),


                        };
                    }
                }
            }

            return FixedArea;

        }

        public static List<ReferanceDto> ReferanceList()
        {
            List<ReferanceDto> list = new List<ReferanceDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Referances where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ReferanceDto Referance = new ReferanceDto()
                            {

                                Name = dr.IsNull("Name") ? "" : dr["Name"].ToString(),
                                Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                                Image = dr.IsNull("Image") ? "" : dr["Image"].ToString(),
                                RowNumber = dr.IsNull("RowNumber") ? 0 : Convert.ToInt32(dr["RowNumber"]),

                            };
                            list.Add(Referance);
                        }
                    }
                }
            }
            return list;

        }

        public static CoverImageDto GetCoverImages()
        {
            CoverImageDto CoverImage = new CoverImageDto();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from coverImages", CommandType.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        CoverImage = new CoverImageDto()
                        {

                            Blog = dr.IsNull("Blog") ? "" : dr["Blog"].ToString(),
                            Contact = dr.IsNull("Contact") ? "" : dr["Contact"].ToString(),
                            Duty = dr.IsNull("Duty") ? "" : dr["Duty"].ToString(),
                            HumanResource = dr.IsNull("HumanResource") ? "" : dr["HumanResource"].ToString(),
                            News = dr.IsNull("News") ? "" : dr["News"].ToString(),
                            Product = dr.IsNull("Product") ? "" : dr["Product"].ToString(),

                        };
                    }
                }
            }

            return CoverImage;

        }



        #region Layout için metodlar
        public static List<PageDto> ShortPageList()
        {
            List<PageDto> list = new List<PageDto>();
            var langId = LanguageOperation.GetLang().Id;

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Pages where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", langId));
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
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                            };
                            list.Add(Page);
                        }
                    }
                }
            }
            return list;

        }

        public static List<DutyDto> ShortDutyList()
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
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()

                            };
                            list.Add(Duty);
                        }
                    }
                }
            }
            return list;

        }

        public static List<ProductDto> ShortProductList()
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
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                            };
                            list.Add(Product);
                        }
                    }
                }
            }
            return list;

        }

        public static List<ProductDto> ShortNewsList()
        {
            List<ProductDto> list = new List<ProductDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from News where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
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
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString()
                            };
                            list.Add(Product);
                        }
                    }
                }
            }
            return list;

        }


        public static List<BlogDto> ShortBlogListLast3()
        {
            List<BlogDto> list = new List<BlogDto>();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select top 3 * from Blogs where State=1 and LanguageId=@LanguageId order by Id desc ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
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
                                ImageBig = dr.IsNull("ImageBig") ? "" : dr["ImageBig"].ToString(),
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                                CreatedDate = dr.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(dr["CreatedDate"])
                            };
                            list.Add(Blog);
                        }
                    }
                }
            }
            return list;
        }

        public static PageDto ShortFirstPageAboutUs()
        {
            PageDto item = new PageDto();

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select top 1 * from Pages where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", LanguageOperation.GetLang().Id));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        item = new PageDto()
                        {
                            Caption = dr.IsNull("Caption") ? "" : dr["Caption"].ToString(),
                            Description = dr.IsNull("Description") ? "" : dr["Description"].ToString(),
                        };


                    }
                }
            }
            return item;
        }

        public static ContactInfoDto ShortGetLogo()
        {
            ContactInfoDto ContactInfo = new ContactInfoDto();
            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from ContactInfoes", CommandType.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ContactInfo = new ContactInfoDto()
                        {
                            Image = dr.IsNull("Image") ? "" : dr["Image"].ToString(),
                        };
                    }
                }
            }

            return ContactInfo;

        }

        public static List<BlogDto> ShortBlogList()
        {
            List<BlogDto> list = new List<BlogDto>();
            var langId = LanguageOperation.GetLang().Id;

            using (SqlProgress sql = new SqlProgress())
            {
                var dt = sql.GetDataTable("select * from Blogs where State=1 and LanguageId=@LanguageId order by RowNumber ", CommandType.Text, new SqlParameter("@LanguageId", langId));
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
                                Url = dr.IsNull("Url") ? "" : dr["Url"].ToString(),
                            };
                            list.Add(Blog);
                        }
                    }
                }
            }
            return list;

        }

        public static int CountBlogComment(int id)
        {
            var result = 0;
            using(SqlProgress sql = new SqlProgress())
            {
                var dt = sql.SetExecuteScalar("select COUNT(*) from BlogComments where BlogId=@BlogId and State=1", CommandType.Text, new SqlParameter("@BlogId", id));
                if (dt!=null)
                {
                    result = Convert.ToInt32(dt);
                }
            }
            return result;
        }

        #endregion





    }
}
