using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(GaleriIslemViewModelMetaData))]
    public partial class GaleriIslemViewModel
    {
       
    }

    public partial class GaleriIslemViewModelMetaData
    {
        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Link { get; set; }
    }

}