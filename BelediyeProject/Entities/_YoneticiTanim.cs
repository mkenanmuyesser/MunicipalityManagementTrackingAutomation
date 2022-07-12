using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(YoneticiTanimIslemViewModelMetaData))]
    public partial class YoneticiTanimIslemViewModel
    {
    }

    public partial class YoneticiTanimIslemViewModelMetaData
    {
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string AdiSoyadi { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Html)]
        [MaxLength(5000, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Aciklama { get; set; }
    }

}