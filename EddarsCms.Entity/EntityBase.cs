using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity
{
    public class EntityBase
    {
        DateTime date = DateTime.Now;

        public int Id { get; set; }
        public int LanguageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int RowNumber { get; set; }
        public bool State { get; set; }

        //id,updatedate,createdate,rownumber
    }
}
