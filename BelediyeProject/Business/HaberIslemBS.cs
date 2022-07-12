using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class HaberIslemBS
    {
 
        public static HaberIslemViewModel HaberGetir(int key)
        {
            HaberIslemViewModel haberIslemViewModel = new HaberIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var haber = entities.Habers.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.HaberKey == key);

                haberIslemViewModel.HaberKey = haber.HaberKey;
                haberIslemViewModel.Baslik = haber.Baslik;
                haberIslemViewModel.Aciklama = haber.Aciklama;
                haberIslemViewModel.DosyaYolu = haber.DosyaYolu;
                haberIslemViewModel.Tarih = haber.Tarih;
                haberIslemViewModel.AktifMi = haber.AktifMi;
            }

            return haberIslemViewModel;
        }

        public static List<Haber> HaberListGetir()
        {

            List<Haber> haberler = new List<Haber>();

            using (DBEntities entities = new DBEntities())
            {
                haberler = entities.Habers.
                                            AsNoTracking().
                                            OrderByDescending(p => p.Tarih).
                                            ToList();
            }

            return haberler;
        }

        public static bool HaberSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Haber haber = entities.Habers.Single(p => p.HaberKey == key);

                    #region validation                   

                    #endregion

                    haber.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    haber.GuncelleTarih = DateTime.Now;
                    haber.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool HaberKaydetGuncelle(HaberIslemViewModel haberIslemViewModel,string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Haber haber = null;

                    if (haberIslemViewModel.HaberKey == 0 || haberIslemViewModel.HaberKey == -1)
                    {
                        haber = new Haber
                        {
                            Baslik = haberIslemViewModel.Baslik,
                            Aciklama = haberIslemViewModel.Aciklama,
                            Tarih = haberIslemViewModel.Tarih,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = haberIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = haberIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            haber.DosyaYolu = dosyaAdi;
                        }

                        entities.Habers.Add(haber);

                    }
                    else
                    {
                        haber = entities.Habers.Single(p => p.HaberKey == haberIslemViewModel.HaberKey);
                        haber.Baslik = haberIslemViewModel.Baslik;
                        haber.Aciklama = haberIslemViewModel.Aciklama;
                        haber.Tarih = haberIslemViewModel.Tarih;

                        HttpPostedFileBase dosya = haberIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            haber.DosyaYolu = dosyaAdi;
                        }

                        haber.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        haber.GuncelleTarih = DateTime.Now;
                        haber.AktifMi = haberIslemViewModel.AktifMi;
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

        public static bool HaberResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Haber haber = entities.Habers.Single(p => p.HaberKey == key);

                    haber.DosyaYolu = null;
                    haber.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    haber.GuncelleTarih = DateTime.Now;
                    haber.AktifMi = false;

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