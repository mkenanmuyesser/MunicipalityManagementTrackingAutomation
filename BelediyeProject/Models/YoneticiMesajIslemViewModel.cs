using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class YoneticiMesajIslemViewModel : YoneticiMesaj
    {
        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

        public List<YoneticiMesaj> YoneticiMesajList
        {
            get
            {
                return YoneticiMesajIslemBS.YoneticiMesajListGetir();
            }
        }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}