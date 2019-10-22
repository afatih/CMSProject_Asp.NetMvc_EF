using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto
{
    public class DtoBase
    {
        public DtoBase()
        {
            UpdatedDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            State = true;
        }



        public int Id { get; set; }
        public int? LanguageId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? RowNumber { get; set; }
        public bool? State { get; set; }
    }
}
