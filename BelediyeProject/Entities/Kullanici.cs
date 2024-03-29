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
    
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            this.Dosyas = new HashSet<Dosya>();
            this.DosyaHashes = new HashSet<DosyaHash>();
            this.KullaniciBirims = new HashSet<KullaniciBirim>();
        }
    
        public System.Guid KullaniciKey { get; set; }
        public int KullaniciRolTipKey { get; set; }
        public Nullable<int> KullaniciIsletimSistemiTipKey { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string MacAdres { get; set; }
        public string RegisterId { get; set; }
        public string DeviceToken { get; set; }
        public string UserId { get; set; }
        public System.Guid KayitKisiKey { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public System.Guid GuncelleKisiKey { get; set; }
        public System.DateTime GuncelleTarih { get; set; }
        public bool AktifMi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dosya> Dosyas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DosyaHash> DosyaHashes { get; set; }
        public virtual tt_KullaniciIsletimSistemiTip tt_KullaniciIsletimSistemiTip { get; set; }
        public virtual tt_KullaniciRolTip tt_KullaniciRolTip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KullaniciBirim> KullaniciBirims { get; set; }
    }
}
