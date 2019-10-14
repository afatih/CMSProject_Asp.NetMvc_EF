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

        public EddarsCmsDbContext():base("EddarsDbCon")
        {

        }

    }
}
