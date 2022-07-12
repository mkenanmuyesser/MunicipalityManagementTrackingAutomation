using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(ReklamIslemViewModelMetaData))]
    public partial class ReklamIslemViewModel
    {
    }

    public partial class ReklamIslemViewModelMetaData
    {

        [MaxLength(200, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string DosyaYolu { get; set; }

        [MaxLength(200, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Link { get; set; }

        [Required(ErrorMessage = "*")]
        public string BaslangicTarihi { get; set; }

        [Required(ErrorMessage = "*")]
        public string BitisTarihi { get; set; }

    }

}