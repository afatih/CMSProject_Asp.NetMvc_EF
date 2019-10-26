using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class ContactInfo:EntityBase
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string MapLocation { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }

    }
}
