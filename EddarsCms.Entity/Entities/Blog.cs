using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class Blog : EntityBase
    {

        public string Caption { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public string BlogBegin { get; set; }
        public string Content { get; set; }
        public bool AcceptComment { get; set; }

    }
}
