using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(KullaniciIslemViewModelMetaData))]
    public partial class KullaniciIslemViewModel
    {
    }

    public partial class KullaniciIslemViewModelMetaData
    {
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "*")]
        public int KullaniciRolTipKey { get; set; }
    }

}