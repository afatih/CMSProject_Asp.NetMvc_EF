using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class User
    {
        public User()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
