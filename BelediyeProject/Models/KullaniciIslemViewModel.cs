using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class KullaniciIslemViewModel : Kullanici
    {

        public List<DosyaGonderilecekBirimTipData> SecilenDosyaGonderilecekBirimTipList { get; set; }
        public string[] DosyaGonderilecekBirimTipKeys { get; set; }

        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

        public List<Kullanici> YonetimKullanicilari
        {
            get
            {
                return KullaniciIslemBS.KullaniciDetayliGetir(2, 4, 5);
            }
        }

        public List<Kullanici> ProgramKullanicilari
        {
            get
            {
                return KullaniciIslemBS.KullaniciDetayliGetir(3);
            }
        }

        public List<tt_KullaniciRolTip> KullaniciRolTipList
        {
            get
            {
                return KullaniciIslemBS.KullaniciRolTipGetir();
            }
        }

        public List<DosyaGonderilecekBirimTipData> DosyaGonderilecekBirimTipList
        {
            get
            {
                return KullaniciIslemBS.DosyaGonderilecekBirimTipGetir();
            }
        }

    }
}