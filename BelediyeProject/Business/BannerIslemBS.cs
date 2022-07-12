using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class BannerIslemBS
    {
        public static BannerIslemViewModel BannerGetir(int key)
        {
            BannerIslemViewModel bannerIslemViewModel = new BannerIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var banner = entities.Banners.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.BannerKey == key);

                bannerIslemViewModel.BannerKey = banner.BannerKey;
                bannerIslemViewModel.Link = banner.Link;
                bannerIslemViewModel.DosyaYolu = banner.DosyaYolu;
                bannerIslemViewModel.AktifMi = banner.AktifMi;
            }

            return bannerIslemViewModel;
        }

        public static List<Banner> BannerListGetir()
        {

            List<Banner> bannerler = new List<Banner>();

            using (DBEntities entities = new DBEntities())
            {
                bannerler = entities.Banners.
                                            AsNoTracking().                                           
                                            ToList();
            }

            return bannerler;
        }

        public static bool BannerSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Banner banner = entities.Banners.Single(p => p.BannerKey == key);

                    #region validation                   

                    #endregion

                    banner.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    banner.GuncelleTarih = DateTime.Now;
                    banner.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool BannerKaydetGuncelle(BannerIslemViewModel bannerIslemViewModel, string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Banner banner = null;

                    if (bannerIslemViewModel.BannerKey == 0 || bannerIslemViewModel.BannerKey == -1)
                    {
                        banner = new Banner
                        {
                            Link = bannerIslemViewModel.Link,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = bannerIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = bannerIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            banner.DosyaYolu = dosyaAdi;
                        }

                        entities.Banners.Add(banner);

                    }
                    else
                    {
                        banner = entities.Banners.Single(p => p.BannerKey == bannerIslemViewModel.BannerKey);
                        banner.Link = bannerIslemViewModel.Link;

                        HttpPostedFileBase dosya = bannerIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            banner.DosyaYolu = dosyaAdi;
                        }

                        banner.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        banner.GuncelleTarih = DateTime.Now;
                        banner.AktifMi = bannerIslemViewModel.AktifMi;
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

        public static bool BannerResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Banner banner = entities.Banners.Single(p => p.BannerKey == key);

                    banner.DosyaYolu = null;
                    banner.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    banner.GuncelleTarih = DateTime.Now;
                    banner.AktifMi = false;

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