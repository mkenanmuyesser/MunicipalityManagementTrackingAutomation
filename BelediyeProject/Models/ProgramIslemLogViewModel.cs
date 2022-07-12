using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public class ProgramIslemLogViewModel : ProgramLog
    {

        public List<ProgramLog> ProgramLogList { get; set; }

        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

    }
}