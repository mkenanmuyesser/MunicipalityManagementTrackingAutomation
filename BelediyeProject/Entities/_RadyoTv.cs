using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(RadyoTvIslemViewModelMetaData))]
    public partial class RadyoTvIslemViewModel
    {
       
    }

    public partial class RadyoTvIslemViewModelMetaData
    {
        [Required(ErrorMessage = "*")]
        public int RadyoTvTipKey { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Link { get; set; }
    }

}