using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class LanguageDto:DtoBase
    {
        //[Required(ErrorMessage = "Maximum oyuncu kapasitesini girmelisiniz")
        //, Range(5, 100, ErrorMessage = "En az 5 en fazla 100 oyuncu olabilir")]

        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string Url { get; set; }


        public string Image { get; set; }
    }
}
