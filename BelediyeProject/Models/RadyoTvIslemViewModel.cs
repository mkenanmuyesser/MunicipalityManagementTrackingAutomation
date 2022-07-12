using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class RadyoTvIslemViewModel:RadyoTv
    {
        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

        public List<RadyoTv> RadyoTvList
        {
            get
            {
                return RadyoTvIslemBS.RadyoTvListGetir();
            }
        }

        public List<tt_RadyoTvTip> RadyoTvTipList
        {
            get
            {
                return RadyoTvIslemBS.RadyoTvTipGetir();
            }
        }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}