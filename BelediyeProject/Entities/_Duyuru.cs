using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(DuyuruIslemViewModelMetaData))]
    public partial class DuyuruIslemViewModel
    {
    }

    public partial class DuyuruIslemViewModelMetaData
    {
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(1000, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Ozet { get; set; }

    }

}