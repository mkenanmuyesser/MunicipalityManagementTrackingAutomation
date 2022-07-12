using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class GaleriIslemBS
    {
        public static GaleriIslemViewModel GaleriGetir(int key)
        {
            GaleriIslemViewModel galeriIslemViewModel = new GaleriIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var Galeri = entities.Galeris.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.GaleriKey == key);

                galeriIslemViewModel.GaleriKey = Galeri.GaleriKey;
                galeriIslemViewModel.Link = Galeri.Link;
                galeriIslemViewModel.DosyaYolu = Galeri.DosyaYolu;
                galeriIslemViewModel.AktifMi = Galeri.AktifMi;
            }

            return galeriIslemViewModel;
        }

        public static List<Galeri> GaleriListGetir()
        {

            List<Galeri> galeriler = new List<Galeri>();

            using (DBEntities entities = new DBEntities())
            {
                galeriler = entities.Galeris.
                                            AsNoTracking().                                           
                                            ToList();
            }

            return galeriler;
        }

        public static bool GaleriSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Galeri galeri = entities.Galeris.Single(p => p.GaleriKey == key);

                    #region validation                   

                    #endregion

                    galeri.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    galeri.GuncelleTarih = DateTime.Now;
                    galeri.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool GaleriKaydetGuncelle(GaleriIslemViewModel galeriIslemViewModel, string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Galeri Galeri = null;

                    if (galeriIslemViewModel.GaleriKey == 0 || galeriIslemViewModel.GaleriKey == -1)
                    {
                        Galeri = new Galeri
                        {
                            Link = galeriIslemViewModel.Link,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = galeriIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = galeriIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            Galeri.DosyaYolu = dosyaAdi;
                        }

                        entities.Galeris.Add(Galeri);

                    }
                    else
                    {
                        Galeri = entities.Galeris.Single(p => p.GaleriKey == galeriIslemViewModel.GaleriKey);
                        Galeri.Link = galeriIslemViewModel.Link;

                        HttpPostedFileBase dosya = galeriIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            Galeri.DosyaYolu = dosyaAdi;
                        }

                        Galeri.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        Galeri.GuncelleTarih = DateTime.Now;
                        Galeri.AktifMi = galeriIslemViewModel.AktifMi;
                    }

                    entities.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool GaleriResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Galeri galeri = entities.Galeris.Single(p => p.GaleriKey == key);

                    galeri.DosyaYolu = null;
                    galeri.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    galeri.GuncelleTarih = DateTime.Now;
                    galeri.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}