using EddarsCms.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.DAL
{
    public class EddarsCmsDbContext:DbContext
    {
        public DbSet<Language> Languages{ get; set; }
        public DbSet<Page> Pages{ get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Referance> Referances { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactMail> ContactMails{ get; set; }
        public DbSet<HumanResource> HumanResources{ get; set; }
        public DbSet<InformationFromUs> InformationFromUs{ get; set; }
        public DbSet<MailInfo> MailInfos { get; set; }

        public EddarsCmsDbContext():base("EddarsDbCon")
        {

        }

    }
}
