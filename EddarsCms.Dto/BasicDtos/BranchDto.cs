using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class BranchDto:DtoBase
    {
        public string Name { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Adress { get; set; }
        public string MapLocation { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
    }
}
