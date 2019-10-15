using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class ContactInfoDto:DtoBase
    {
        public string Adress { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string MapLocation { get; set; }
        public string Image { get; set; }
    }
}
