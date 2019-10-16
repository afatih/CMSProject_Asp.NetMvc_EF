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

            public EntityBase()
            {
                CreatedDate = DateTime.Now;
                UpdatedDate = DateTime.Now;
                State = true;
            }

            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public int? RowNumber { get; set; }



            public DateTime? CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public bool? State { get; set; }

            //id,updatedate,createdate,rownumber
        }
    }
