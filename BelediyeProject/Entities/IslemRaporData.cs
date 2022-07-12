using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Entities
{
    public class IslemRaporData
    {
        public string DosyaAcilmaNedenTipAdi { get; set; }
        public string DosyaDurumTipAdi { get; set; }
        public string DosyaGonderilecekBirimTipAdi { get; set; }
        public string DosyaMesajTipAdi { get; set; }
        public string Mesaj { get; set; }
        public string Aciklama { get; set; }
        public DateTime IslemZamani { get; set; }
        public bool AktifMi { get; set; }
    }
}