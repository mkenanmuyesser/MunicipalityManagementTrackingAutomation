using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class LogoIslemViewModel : Logo
    {
        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

    }
}