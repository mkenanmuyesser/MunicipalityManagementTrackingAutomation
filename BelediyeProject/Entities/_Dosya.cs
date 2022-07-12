using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(DosyaIslemViewModelMetaData))]
    public partial class DosyaIslemViewModel
    {

    }

    public partial class DosyaIslemViewModelMetaData
    {
        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Aciklama { get; set; }

        [MaxLength(100, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Bildirim { get; set; }
    }

}