using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class BlogComment 
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int RowNumber { get; set; }
        public int LanguageId { get; set; }
        public bool State{ get; set; }
    }
}
