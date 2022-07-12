using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public class RaporIslemViewModel
    {
        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

        #region Islem Raporları

        public DateTime IslemRaporBaslangic { get; set; }
        public DateTime IslemRaporBitis { get; set; }
        public int IslemRaporDosyaAcilmaNedenTipKey { get; set; }
        public int IslemRaporDosyaDurumTipKey { get; set; }
        public int IslemRaporDosyaGonderilenBirimTipKey { get; set; }
        public int IslemRaporDosyaMesajTipKey { get; set; }
        public string IslemRaporMesaj { get; set; }
        public string IslemRaporAciklama { get; set; }
        public bool IslemRaporAktifMi { get; set; }


        public List<tt_DosyaAcilmaNedenTip> IslemRaporDosyaAcilmaNedenTipler
        {
            get
            {
               return RaporIslemBS.DosyaAcilmaNedenTipList();
            }
        }
        public List<tt_DosyaDurumTip> IslemRaporDosyaDurumTipler
        {
            get
            {
                return RaporIslemBS.DosyaDurumTipList();
            }
        }
        public List<tt_DosyaGonderilecekBirimTip> IslemRaporDosyaGonderilenBirimTipler
        {
            get
            {
                return RaporIslemBS.DosyaGonderilecekBirimTipList();
            }
        }
        public List<tt_DosyaMesajTip> IslemRaporDosyaMesajTipler
        {
            get
            {
                return RaporIslemBS.DosyaMesajTipList();
            }
        }
        public List<IslemRaporData> IslemRaporListeler { get; set; }      

        #endregion

        #region Birime Gore Kullanım Raporları

        public DateTime BirimeGoreKullanimRaporBaslangic { get; set; }
        public DateTime BirimeGoreKullanimRaporBitis { get; set; }

        public List<IslemRaporData> BirimeGoreKullanimRaporListeler { get; set; }

        #endregion

    }
}