//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BelediyeProject.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Duyuru
    {
        public int DuyuruKey { get; set; }
        public string Baslik { get; set; }
        public string Ozet { get; set; }
        public System.DateTime Tarih { get; set; }
        public System.Guid KayitKisiKey { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public System.Guid GuncelleKisiKey { get; set; }
        public System.DateTime GuncelleTarih { get; set; }
        public bool AktifMi { get; set; }
    }
}
