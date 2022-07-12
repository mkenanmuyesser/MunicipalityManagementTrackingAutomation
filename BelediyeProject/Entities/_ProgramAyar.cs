using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(ProgramIslemAyarViewModelMetaData))]
    public partial class ProgramIslemAyarViewModel
    {
    }

    public partial class ProgramIslemAyarViewModelMetaData
    {
        [Required(ErrorMessage ="*")]
        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Kisaltma { get; set; }

        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string ResimUrl { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string ServerAdres { get; set; }

        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string SayfaAdres { get; set; }

        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string GoogleApiKey { get; set; }

        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string AppleApiKey { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string KeyCode { get; set; }
    }

}