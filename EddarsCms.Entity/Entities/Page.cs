using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class Page : EntityBase
    {
        public string Caption { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string Content { get; set; }

        public string ImageCover { get; set; }
        public string ImageBig { get; set; }

        public string Description { get; set; }
    }
}
