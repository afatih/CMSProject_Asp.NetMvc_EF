using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class MultimediaDto:DtoBase
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string FolderPath { get; set; }
    }
}
