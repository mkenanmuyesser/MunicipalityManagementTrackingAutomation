﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(BannerIslemViewModelMetaData))]
    public partial class BannerIslemViewModel
    {
       
    }

    public partial class BannerIslemViewModelMetaData
    {
        [MaxLength(500, ErrorMessage = "Karakter sınırı aşıldı!")]
        public string Link { get; set; }
    }

}