using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(CenazeIslemViewModelMetaData))]
    public partial class CenazeIslemViewModel
    {
    }

    public partial class CenazeIslemViewModelMetaData
    {
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string AdiSoyadi { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string BabaAdi { get; set; }

        [Required(ErrorMessage = "*")]
        public string Tarih { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string DogumYeri { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Iletisim { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Html)]
        [MaxLength(1000, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Aciklama { get; set; }

    }

}