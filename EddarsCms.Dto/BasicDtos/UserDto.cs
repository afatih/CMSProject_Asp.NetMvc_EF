using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class UserDto
    {
        public UserDto()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmanız zorunludur")]
        public string Password { get; set; }


        public string Password2 { get; set; }
        public string EMail { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
