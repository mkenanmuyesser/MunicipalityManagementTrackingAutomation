using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class ProjeEtkinlikIslemViewModel : ProjeEtkinlik
    {
        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

        public List<ProjeEtkinlik> ProjeEtkinlikList
        {
            get
            {
                return ProjeEtkinlikIslemBS.ProjeEtkinlikListGetir();
            }
        }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}