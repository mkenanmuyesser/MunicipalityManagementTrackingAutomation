using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Entities
{
    public class MenuData
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Link { get; set; }
        public bool AktifMi { get; set; }
        public int? UstMenuId { get; set; }
    }
}