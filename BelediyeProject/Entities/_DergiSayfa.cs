using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{

    [MetadataType(typeof(DergiSayfaIslemViewModelMetaData))]
    public partial class DergiSayfaIslemViewModel
    {

    }

    public partial class DergiSayfaIslemViewModelMetaData
    {
        [Required(ErrorMessage = "*")]
        public int SayfaNo { get; set; }
    }

}