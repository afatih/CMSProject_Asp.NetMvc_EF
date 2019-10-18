using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class SliderDto:DtoBase
    {
        public string Caption { get; set; }
        public string Description { get; set; }
        public string ButtonText { get; set; }
        public string Url { get; set; }
        public bool OpenNewTab { get; set; }

        public string ImageCover { get; set; }
        public string ImageBig { get; set; }
    }
}
