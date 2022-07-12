using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(TanimIslemBirimViewModelMetaData))]
    public partial class TanimIslemBirimViewModel
    {
    }

    public partial class TanimIslemBirimViewModelMetaData
    {
        [Required(ErrorMessage ="*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string DosyaGonderilecekBirimTipAdi { get; set; }        
    }

}