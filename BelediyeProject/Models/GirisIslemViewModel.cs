using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;

namespace BelediyeProject.Models
{

    public class GirisIslemViewModel
    {
        public ProgramAyar ProgramAyar
        {
            get
            {
                return ProgramIslemAyarBS.ProgramAyarGetir();
            }
        }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Sonuc { get; set; }
    }
}
