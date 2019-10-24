using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class FixedAreaDto
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
        public string AnaSayfaUrunlerimiz { get; set; }
        public string AnaSayfaIletisim { get; set; }
        public string AnaSayfaInsanKaynaklari { get; set; }
        public string AnaSayfaUrunlerBaslik { get; set; }
        public string AnaSayfaBlogBaslik { get; set; }
        public string AnaSayfaHaberOku { get; set; }

        public string FooterEnSonBloglar { get; set; }
        public string FooterHaberdarOl { get; set; }
        public string FooterHaberdarOlAciklama { get; set; }
        public string FooterHaberdarOlEPosta { get; set; }
        public string FooterInstagram { get; set; }


        public string InsanKaynaklariBaslik { get; set; }
        public string InsanKaynaklariAd { get; set; }
        public string InsanKaynaklariSoyad { get; set; }
        public string InsanKaynaklariMail { get; set; }
        public string InsanKaynaklariTelefon { get; set; }
        public string InsanKaynaklariDosyaSec { get; set; }
        public string InsanKaynaklariMesaj { get; set; }
        public string InsanKaynaklariGonder { get; set; }
        public string InsanKaynaklariIcerik { get; set; }
    }
}
