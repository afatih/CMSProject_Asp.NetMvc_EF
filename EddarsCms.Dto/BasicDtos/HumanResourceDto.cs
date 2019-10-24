using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class HumanResourceDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Bu alanın doldurulması zorunludur")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur")]
        public string Surname { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur")]
        public string Mail { get; set; }
        public string Message { get; set; }
        public string CV { get; set; }
        public DateTime Date { get; set; }
    }
}
