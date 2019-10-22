using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class SocialMedia:EntityBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string IconFull { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
    }
}
