using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BelediyeProject.Entities
{
    public class MobileKullaniciData
    {
        [Required]
        public int KullaniciIsletimSistemiTipKey { get; set; }

        [Required]
        public string MacAdres { get; set; }

        [Required]
        public string RegisterId { get; set; }

        public string DeviceToken { get; set; }

        public string KullaniciAdi { get; set; }

        public string UserId { get; set; }
    }

    public class MobileDosyaData : MobileKullaniciData
    {
        [Required]
        public int DosyaAcilmaNedenTipKey { get; set; }

        public int? DosyaGonderilecekBirimTipKey { get; set; }

        public int? DosyaMesajTipKey { get; set; }

        public int? DosyaKonumTipKey { get; set; }

        public string Mesaj { get; set; }

        public string IpAdres { get; set; }

        public string Enlem { get; set; }

        public string Boylam { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public string IrtibatNo { get; set; }

        public string Resimler { get; set; }

        public string Videolar { get; set; }

    }

    public class MobileDosyaHashData : MobileKullaniciData
    {
        public string IpAdres { get; set; }

        public string Enlem { get; set; }

        public string Boylam { get; set; }

        [Required]
        public string HashBilgi { get; set; }
    }

    public class MobileDosyaDurumData
    {
        public DateTime IslemZamani { get; set; }

        public string DosyaAcilmaNedenTipAdi { get; set; }

        public string DosyaDurumTipAdi { get; set; }

        public string DosyaGonderilecekBirimTipAdi { get; set; }

        public string DosyaMesajTipAdi { get; set; }

        public string Mesaj { get; set; }

    }

    public class MobileReklamData
    {
        public string DosyaYolu { get; set; }

        public string Link { get; set; }
    }

    public class MobileDuyuruData
    {
        public string Baslik { get; set; }

        public string Ozet { get; set; }

        public DateTime Tarih { get; set; }
    }

    public class MobileYoneticiData
    {
        public string AdiSoyadi { get; set; }

        public string Aciklama { get; set; }
    }

    public class MobileYoneticiMesajData
    {
        public string Baslik { get; set; }

        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }

        public string DosyaYolu { get; set; }
    }

    public class MobileHaberData
    {
        public string Baslik { get; set; }

        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }

        public string DosyaYolu { get; set; }
    }

    public class MobileCenazeData
    {
        public string AdiSoyadi { get; set; }

        public string BabaAdi { get; set; }

        public DateTime Tarih { get; set; }

        public string DogumYeri { get; set; }

        public string Iletisim { get; set; }

        public string Aciklama { get; set; }

    }

    public class MobileLogoResimData
    {
        public string DosyaYolu { get; set; }
    }

    public class MobileSayfaAdresData
    {
        public string SayfaAdres { get; set; }
    }

    public class MobileBannerData
    {
        public string Link { get; set; }
        public string DosyaYolu { get; set; }
    }

    public class MobileGaleriData
    {
        public string Link { get; set; }
        public string DosyaYolu { get; set; }
    }

    public class MobileRadyoTvData
    {
        public string Adi { get; set; }
        public string Link { get; set; }
        public string DosyaYolu { get; set; }
    }

    public class MobileDergiData
    {
        public int DergiKey { get; set; }
        public string Adi { get; set; }       
        public string DosyaYolu { get; set; }
    }

    public class MobileDergiSayfaData
    {
        public int SayfaNo { get; set; }
        public string DosyaYolu { get; set; }
    }

    public class MobileProjeEtkinlikData
    {
        public string Baslik { get; set; }

        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; }

        public string DosyaYolu { get; set; }
    }

    public class MobileLimitData
    {
        public bool LimitIzin { get; set; }
    }
}