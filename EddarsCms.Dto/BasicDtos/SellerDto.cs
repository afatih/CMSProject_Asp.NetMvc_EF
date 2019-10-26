using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class SellerDto:DtoBase
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string AuthorityPlace { get; set; }
        public string Mail { get; set; }
        public string Image { get; set; }
        public string MapLocation { get; set; }
        public string Description { get; set; }
    }
}
