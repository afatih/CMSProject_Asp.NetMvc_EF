using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto
{
    public class DtoBase
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public DateTime CreatedDate { get { return DateTime.Now; } set { } }
        public DateTime UpdatedDate { get { return DateTime.Now; } set { } }
        public int RowNumber { get; set; }
        public bool State { get; set; }
        public bool DefaultState { get { return true; } set { } }
    }
}
