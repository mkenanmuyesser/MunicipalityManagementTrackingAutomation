using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class DuyuruIslemViewModel : Duyuru
    {
        public List<MenuData> Menu
        {
            get
            {                
                return AnaSayfaBS.MenuOlustur();
            }
        }

        public List<Duyuru> DuyuruList
        {
            get
            {
                return DuyuruIslemBS.DuyuruListGetir();
            }
        }
    }
}