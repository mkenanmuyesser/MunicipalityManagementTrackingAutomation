using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public class AnaSayfaViewModel
    {
        public ProgramAyar ProgramAyar
        {
            get
            {
                return ProgramIslemAyarBS.ProgramAyarGetir();
            }
        }
        public int KullaniciRolTipKey { get; set; }
       

        public List<MenuData> Menu()
        {
            return AnaSayfaBS.MenuOlustur();
        }
    }
}