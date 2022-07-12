using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(HaberIslemViewModelMetaData))]
    public partial class HaberIslemViewModel
    {
       
    }

    public partial class HaberIslemViewModelMetaData
    {
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Html)]
        [MaxLength(1000, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Aciklama { get; set; }

        [Required(ErrorMessage = "*")]
        public DateTime Tarih { get; set; }

    }

}