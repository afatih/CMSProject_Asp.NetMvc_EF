using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class ProductDto:DtoBase
    {
        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string Name { get; set; }
        public int MainProdId { get; set; }
        public string MainProdName { get; set; }


        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public int MainCatId { get; set; }
        public string MainCatName { get; set; }
        public string Caption { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string ImageBig { get; set; }
        public string ImageCover { get; set; }
        public string ImageSmall { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Video1 { get; set; }
        public string Video2 { get; set; }
        public string Video3 { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string Url { get; set; }
    }
}
