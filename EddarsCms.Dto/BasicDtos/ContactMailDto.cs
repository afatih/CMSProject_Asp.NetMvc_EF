using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class ContactMailDto
    {
        public ContactMailDto()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Bu alanın doldurulması zorunludur")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur")]
        public string Mail { get; set; }
        public string Caption { get; set; }
        public string Content { get; set; }

    }
}
