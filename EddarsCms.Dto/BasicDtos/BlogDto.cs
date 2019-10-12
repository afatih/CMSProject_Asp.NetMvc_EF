using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class BlogDto:DtoBase
    {
        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string Caption { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string Url { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string BlogBegin { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string Content { get; set; }

        public bool AcceptComment { get; set; }
    }
}
