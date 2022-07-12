using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Entities
{
    [Serializable]
    public class KullaniciData
    {
        public Guid KullaniciKey { get; set; }
        public int KullaniciRolTipKey { get; set; }
        public string KullaniciAdi { get; set; }
    }
}