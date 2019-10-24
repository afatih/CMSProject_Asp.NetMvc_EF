using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EddarsCms.Entity.Entities
{
    public class HumanResource 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Message { get; set; }
        public string CV { get; set; }
        public DateTime Date { get; set; }

    }
}
