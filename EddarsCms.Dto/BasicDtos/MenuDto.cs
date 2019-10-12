using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class MenuDto:DtoBase
    {
        public string Caption { get; set; }
        public string Url { get; set; }
        public bool OpenNewTab { get; set; }
        public int Number { get; set; }
        public int MainId { get; set; }
    }
}
