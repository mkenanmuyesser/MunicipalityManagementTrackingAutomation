using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class DergiSayfaIslemViewModel : DergiSayfa
    {
        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

        public List<DergiSayfa> DergiSayfaList
        {
            get
            {
                return DergiSayfaIslemBS.DergiSayfaListGetir();
            }
        }

        public List<Dergi> DergiList
        {
            get
            {
                return DergiIslemBS.DergiListGetir();
            }
        }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}