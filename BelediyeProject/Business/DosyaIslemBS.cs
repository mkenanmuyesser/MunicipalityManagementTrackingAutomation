using BelediyeProject.Entities;
using BelediyeProject.Models;
using ExifLib;
using PushSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class DosyaIslemBS
    {
        public static DosyaIslemViewModel DosyaDetaylariGetir(ref DosyaIslemViewModel dosyaIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();
            int? dosyaGonderilecekBirimTipKey = dosyaIslemViewModel.DosyaGonderilecekBirimTipKey;
            DateTime baslangicGelen = dosyaIslemViewModel.BaslangicGelen;
            DateTime bitisGelen = dosyaIslemViewModel.BitisGelen;
            DateTime baslangicIslemYapilmis = dosyaIslemViewModel.BaslangicIslemYapilmis;
            DateTime bitisIslemYapilmis = dosyaIslemViewModel.BitisIslemYapilmis;

            using (DBEntities entities = new DBEntities())
            {
                switch (kullaniciData.KullaniciRolTipKey)
                {
                    case 1:
                    case 2:
                    case 4:
                        dosyaIslemViewModel.GelenDosyalar = entities.Dosyas.
                                                                            AsNoTracking().
                                                                            Include("tt_DosyaAcilmaNedenTip").
                                                                            Include("tt_DosyaGonderilecekBirimTip").
                                                                            Include("tt_DosyaMesajTip").
                                                                            Include("tt_DosyaDurumTip").
                                                                            Include("tt_DosyaKonumTip").
                                                                            ToList().
                                                                            Where(p => p.DosyaGonderilecekBirimTipKey == dosyaGonderilecekBirimTipKey &&
                                                                                       p.DosyaDurumTipKey == 1 &&
                                                                                       baslangicGelen <= p.IslemZamani.Date &&
                                                                                       bitisGelen >= p.IslemZamani.Date).
                                                                            ToList();

                        dosyaIslemViewModel.IslemYapilanDosyalar = entities.Dosyas.
                                                                                   AsNoTracking().
                                                                                   Include("tt_DosyaAcilmaNedenTip").
                                                                                   Include("tt_DosyaGonderilecekBirimTip").
                                                                                   Include("tt_DosyaMesajTip").
                                                                                   Include("tt_DosyaDurumTip").
                                                                                   Include("tt_DosyaKonumTip").
                                                                                   ToList().
                                                                                   Where(p => p.DosyaGonderilecekBirimTipKey == dosyaGonderilecekBirimTipKey &&
                                                                                              p.DosyaDurumTipKey != 1 &&
                                                                                              baslangicIslemYapilmis <= p.IslemZamani.Date &&
                                                                                              bitisIslemYapilmis >= p.IslemZamani.Date).
                                                                                   ToList();
                        break;
                    case 5:
                        dosyaIslemViewModel.GelenDosyalar = entities.Dosyas.
                                                                            AsNoTracking().
                                                                            Include("tt_DosyaAcilmaNedenTip").
                                                                            Include("tt_DosyaGonderilecekBirimTip").
                                                                            Include("tt_DosyaMesajTip").
                                                                            Include("tt_DosyaDurumTip").
                                                                            Include("tt_DosyaKonumTip").
                                                                            ToList().
                                                                            Where(p => p.DosyaGonderilecekBirimTipKey == dosyaGonderilecekBirimTipKey &&
                                                                                       p.DosyaDurumTipKey == 2 &&
                                                                                       baslangicGelen <= p.IslemZamani.Date &&
                                                                                       bitisGelen >= p.IslemZamani.Date).
                                                                            ToList();

                        dosyaIslemViewModel.IslemYapilanDosyalar = entities.Dosyas.
                                                                                   AsNoTracking().
                                                                                   Include("tt_DosyaAcilmaNedenTip").
                                                                                   Include("tt_DosyaGonderilecekBirimTip").
                                                                                   Include("tt_DosyaMesajTip").
                                                                                   Include("tt_DosyaDurumTip").
                                                                                   Include("tt_DosyaKonumTip").
                                                                                   ToList().
                                                                                   Where(p => p.DosyaGonderilecekBirimTipKey == dosyaGonderilecekBirimTipKey &&
                                                                                              (p.DosyaDurumTipKey == 3 ||
                                                                                               p.DosyaDurumTipKey == 4 ||
                                                                                               p.DosyaDurumTipKey == 5) &&
                                                                                              baslangicIslemYapilmis <= p.IslemZamani.Date &&
                                                                                              bitisIslemYapilmis >= p.IslemZamani.Date).
                                                                                   ToList();
                        break;
                }
            }

            return dosyaIslemViewModel;
        }

        public static DosyaIslemViewModel DosyaGetir(int id)
        {
            DosyaIslemViewModel dosyaIslemViewModel = null;

            using (DBEntities entities = new DBEntities())
            {
                var dosya = entities.Dosyas.
                                            AsNoTracking().
                                            Include("tt_DosyaAcilmaNedenTip").
                                            Include("tt_DosyaGonderilecekBirimTip").
                                            Include("tt_DosyaMesajTip").
                                            Include("tt_DosyaDurumTip").
                                            Include("tt_DosyaKonumTip").
                                            Include("DosyaResims").
                                            Include("Kullanici").
                                            SingleOrDefault(p => p.DosyaKey == id);

                dosyaIslemViewModel = new DosyaIslemViewModel
                {
                    Aciklama = dosya.Aciklama,
                    AktifMi = dosya.AktifMi,
                    DosyaAcilmaNedenTipKey = dosya.DosyaAcilmaNedenTipKey,
                    DosyaDurumTipKey = dosya.DosyaDurumTipKey,
                    DosyaGonderilecekBirimTipKey = dosya.DosyaGonderilecekBirimTipKey,
                    DosyaKey = dosya.DosyaKey,
                    DosyaMesajTipKey = dosya.DosyaMesajTipKey,
                    DosyaKonumTipKey = dosya.DosyaKonumTipKey,
                    Enlem = dosya.Enlem,
                    Boylam = dosya.Boylam,
                    Adi = dosya.Adi,
                    Soyadi = dosya.Soyadi,
                    IrtibatNo = dosya.IrtibatNo,
                    GuncelleKisiKey = dosya.GuncelleKisiKey,
                    GuncelleTarih = dosya.GuncelleTarih,
                    IslemZamani = dosya.IslemZamani,
                    KayitKisiKey = dosya.KayitKisiKey,
                    KayitTarih = dosya.KayitTarih,
                    IpAdres = dosya.IpAdres,
                    KullaniciKey = dosya.KullaniciKey,
                    Mesaj = dosya.Mesaj,
                    DosyaResims = dosya.DosyaResims,
                    Kullanici = dosya.Kullanici,
                    tt_DosyaGonderilecekBirimTip = dosya.tt_DosyaGonderilecekBirimTip,
                    tt_DosyaAcilmaNedenTip = dosya.tt_DosyaAcilmaNedenTip,
                    tt_DosyaMesajTip = dosya.tt_DosyaMesajTip,
                    tt_DosyaDurumTip = dosya.tt_DosyaDurumTip,
                    tt_DosyaKonumTip=dosya.tt_DosyaKonumTip,
                };
            }

            return dosyaIslemViewModel;
        }

        public static void BildirimGonder(DosyaIslemViewModel dosyaIslemViewModel)
        {
            var kullanici = DosyaGetir(dosyaIslemViewModel.DosyaKey).Kullanici;

            switch (kullanici.KullaniciIsletimSistemiTipKey)
            {
                case 1:
                    MobileBS.PushNotificationToAndroid(new string[] { kullanici.RegisterId }, "0", dosyaIslemViewModel.Bildirim);
                    break;
                case 2:
                    MobileBS.PushNotificationToIos(kullanici.RegisterId, "0", dosyaIslemViewModel.Bildirim);
                    break;
            }
        }

        public static void KaraListeyeAl(DosyaIslemViewModel dosyaIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            using (DBEntities entities = new DBEntities())
            {
                var kullaniciKey = DosyaGetir(dosyaIslemViewModel.DosyaKey).Kullanici.KullaniciKey;
                var kullanici = entities.Kullanicis.
                                         SingleOrDefault(p => p.KullaniciKey == kullaniciKey);
                if (kullanici != null)
                {
                    kullanici.AktifMi = false;

                    kullanici.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    kullanici.GuncelleTarih = DateTime.Now;

                    entities.SaveChanges();
                }
            }
        }
        public static void DosyaYonlendir(DosyaIslemViewModel dosyaIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            using (DBEntities entities = new DBEntities())
            {
                var dosya = entities.Dosyas.
                                            SingleOrDefault(p => p.DosyaKey == dosyaIslemViewModel.DosyaKey);

                dosya.DosyaDurumTipKey = 2;
                dosya.DosyaGonderilecekBirimTipKey = dosyaIslemViewModel.DosyaGonderilecekBirimTipKey;
                dosya.Aciklama = dosyaIslemViewModel.Aciklama;
                dosya.IslemZamani = DateTime.Now;

                dosya.GuncelleKisiKey = kullaniciData.KullaniciKey;
                dosya.GuncelleTarih = DateTime.Now;

                entities.SaveChanges();
            }
        }

        public static int? DosyaKabulEt(DosyaIslemViewModel dosyaIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();
            int? dosyaGonderilecekBirimTipKey = null;
            using (DBEntities entities = new DBEntities())
            {
                var dosya = entities.Dosyas.
                                            SingleOrDefault(p => p.DosyaKey == dosyaIslemViewModel.DosyaKey);

                dosya.DosyaDurumTipKey = 3;
                dosya.Aciklama = dosyaIslemViewModel.Aciklama;
                dosya.IslemZamani = DateTime.Now;

                dosya.GuncelleKisiKey = kullaniciData.KullaniciKey;
                dosya.GuncelleTarih = DateTime.Now;

                dosyaGonderilecekBirimTipKey = dosya.DosyaGonderilecekBirimTipKey;

                entities.SaveChanges();
            }
            return dosyaGonderilecekBirimTipKey;
        }

        public static int? DosyaReddet(DosyaIslemViewModel dosyaIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();
            int? dosyaGonderilecekBirimTipKey = null;
            using (DBEntities entities = new DBEntities())
            {
                var dosya = entities.Dosyas.
                                            SingleOrDefault(p => p.DosyaKey == dosyaIslemViewModel.DosyaKey);

                dosya.DosyaDurumTipKey = 4;
                dosya.Aciklama = dosyaIslemViewModel.Aciklama;
                dosya.IslemZamani = DateTime.Now;

                dosya.GuncelleKisiKey = kullaniciData.KullaniciKey;
                dosya.GuncelleTarih = DateTime.Now;

                dosyaGonderilecekBirimTipKey = dosya.DosyaGonderilecekBirimTipKey;

                entities.SaveChanges();
            }
            return dosyaGonderilecekBirimTipKey;
        }

        public static int? DosyaSil(DosyaIslemViewModel dosyaIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();
            int? dosyaGonderilecekBirimTipKey = null;
            using (DBEntities entities = new DBEntities())
            {
                var dosya = entities.Dosyas.
                                            SingleOrDefault(p => p.DosyaKey == dosyaIslemViewModel.DosyaKey);

                dosya.DosyaDurumTipKey = 5;
                dosya.Aciklama = dosyaIslemViewModel.Aciklama;
                dosya.IslemZamani = DateTime.Now;

                dosya.GuncelleKisiKey = kullaniciData.KullaniciKey;
                dosya.GuncelleTarih = DateTime.Now;
                dosya.AktifMi = false;

                dosyaGonderilecekBirimTipKey = dosya.DosyaGonderilecekBirimTipKey;

                entities.SaveChanges();
            }
            return dosyaGonderilecekBirimTipKey;
        }

        public static bool ResimHashBilgiDogrulama(int dosyaResimKey)
        {
            bool dogrulama = true;
            using (DBEntities entities = new DBEntities())
            {
                var dosyaResim = entities.DosyaResims.AsNoTracking().
                                                    SingleOrDefault(p => p.DosyaResimKey == dosyaResimKey);
                if (dosyaResim != null)
                {
                    string dosyaYolu = HttpContext.Current.Server.MapPath("~/Uploads/Resim");
                    var path = Path.Combine(dosyaYolu, dosyaResim.DosyaYolu);
                    FileInfo fi = new FileInfo(path);

                    //using (ExifReader reader = new ExifReader(path))
                    //{

                    //}
                    //string hashCode = fi.GetHashCode().ToString();

                    //var dosyaHashBilgiDogrulama = entities.DosyaHashes.AsNoTracking().
                    //                                          Any(p => p.HashBilgi == hashCode);
                    //dogrulama = dosyaHashBilgiDogrulama;
                }
            }

            return dogrulama;
        }

    }
}