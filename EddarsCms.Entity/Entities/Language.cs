using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class Language:EntityBase
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }
    }
}
