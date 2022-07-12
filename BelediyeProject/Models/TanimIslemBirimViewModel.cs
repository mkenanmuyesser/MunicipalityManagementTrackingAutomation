using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class TanimIslemBirimViewModel : tt_DosyaGonderilecekBirimTip
    {      

        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

        public List<tt_DosyaGonderilecekBirimTip> BirimTipleri
        {
            get
            {
                return TanimIslemBirimBS.BirimTipleriGetir();
            }
        }

    }
}