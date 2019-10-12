using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string MapLocation { get; set; }

    }
}
