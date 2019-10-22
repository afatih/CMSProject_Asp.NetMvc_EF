using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Entity.Entities
{
    public class FixedArea
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string IletisimFormBaslik { get; set; }
        public string IletisimIsim { get; set; }
        public string IletisimKonu { get; set; }
        public string IletisimTelefon { get; set; }
        public string IletisimMesaj { get; set; }
        public string IletisimGonder { get; set; }
        public string IletisimBilgiBaslik { get; set; }
        public string IletisimAdresBaslik { get; set; }
        public string AnaSayfa { get; set; }
        public string AnaSayfaKurumsal { get; set; }
        public string AnaSayfaHizmetlerimiz { get; set; }
        public string AnaSayfaIletisim { get; set; }
        public string AnaSayfaUrunlerBaslik { get; set; }
        public string AnaSayfaBlogBaslik { get; set; }


    }
}
