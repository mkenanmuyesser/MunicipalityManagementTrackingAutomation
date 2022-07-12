using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(DergiIslemViewModelMetaData))]
    public partial class DergiIslemViewModel
    {
       
    }

    public partial class DergiIslemViewModelMetaData
    {
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Adi { get; set; }
    }

}