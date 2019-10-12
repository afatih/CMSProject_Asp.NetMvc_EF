using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class Menu : EntityBase
    {
        public string Caption { get; set; }
        public string Url { get; set; }
        public bool OpenNewTab { get; set; }
        public int Number { get; set; }
        public int MainId { get; set; }



        //daha sonra https://www.youtube.com/watch?v=j95PQ2Ebb-c adresindeki gibi özel yada kategoriler tarzı seçenek eklenebilir.
        //public int MenuType { get; set; }


    }
}
