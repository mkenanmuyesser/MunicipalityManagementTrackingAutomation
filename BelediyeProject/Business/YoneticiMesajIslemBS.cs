using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class YoneticiMesajIslemBS
    {
 
        public static YoneticiMesajIslemViewModel YoneticiMesajGetir(int key)
        {
            YoneticiMesajIslemViewModel yoneticiMesajIslemViewModel = new YoneticiMesajIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var yoneticiMesaj = entities.YoneticiMesajs.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.YoneticiMesajKey == key);

                yoneticiMesajIslemViewModel.YoneticiMesajKey = yoneticiMesaj.YoneticiMesajKey;
                yoneticiMesajIslemViewModel.Baslik = yoneticiMesaj.Baslik;
                yoneticiMesajIslemViewModel.Aciklama = yoneticiMesaj.Aciklama;
                yoneticiMesajIslemViewModel.DosyaYolu = yoneticiMesaj.DosyaYolu;
                yoneticiMesajIslemViewModel.Tarih = yoneticiMesaj.Tarih;
                yoneticiMesajIslemViewModel.AktifMi = yoneticiMesaj.AktifMi;
            }

            return yoneticiMesajIslemViewModel;
        }

        public static List<YoneticiMesaj> YoneticiMesajListGetir()
        {

            List<YoneticiMesaj> yoneticiMesajlar = new List<YoneticiMesaj>();

            using (DBEntities entities = new DBEntities())
            {
                yoneticiMesajlar = entities.YoneticiMesajs.
                                            AsNoTracking().
                                            OrderByDescending(p => p.Tarih).
                                            ToList();
            }

            return yoneticiMesajlar;
        }

        public static bool YoneticiMesajSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    YoneticiMesaj yoneticiMesaj = entities.YoneticiMesajs.Single(p => p.YoneticiMesajKey == key);

                    #region validation                   

                    #endregion

                    yoneticiMesaj.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    yoneticiMesaj.GuncelleTarih = DateTime.Now;
                    yoneticiMesaj.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool YoneticiMesajKaydetGuncelle(YoneticiMesajIslemViewModel yoneticiMesajIslemViewModel,string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    YoneticiMesaj yoneticiMesaj = null;

                    if (yoneticiMesajIslemViewModel.YoneticiMesajKey == 0 || yoneticiMesajIslemViewModel.YoneticiMesajKey == -1)
                    {
                        yoneticiMesaj = new YoneticiMesaj
                        {
                            Baslik = yoneticiMesajIslemViewModel.Baslik,
                            Aciklama = yoneticiMesajIslemViewModel.Aciklama,
                            Tarih = yoneticiMesajIslemViewModel.Tarih,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = yoneticiMesajIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = yoneticiMesajIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            yoneticiMesaj.DosyaYolu = dosyaAdi;
                        }

                        entities.YoneticiMesajs.Add(yoneticiMesaj);

                    }
                    else
                    {
                        yoneticiMesaj = entities.YoneticiMesajs.Single(p => p.YoneticiMesajKey == yoneticiMesajIslemViewModel.YoneticiMesajKey);
                        yoneticiMesaj.Baslik = yoneticiMesajIslemViewModel.Baslik;
                        yoneticiMesaj.Aciklama = yoneticiMesajIslemViewModel.Aciklama;
                        yoneticiMesaj.Tarih = yoneticiMesajIslemViewModel.Tarih;

                        HttpPostedFileBase dosya = yoneticiMesajIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            yoneticiMesaj.DosyaYolu = dosyaAdi;
                        }

                        yoneticiMesaj.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        yoneticiMesaj.GuncelleTarih = DateTime.Now;
                        yoneticiMesaj.AktifMi = yoneticiMesajIslemViewModel.AktifMi;
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

        public static bool YoneticiMesajResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    YoneticiMesaj yoneticiMesaj = entities.YoneticiMesajs.Single(p => p.YoneticiMesajKey == key);

                    yoneticiMesaj.DosyaYolu = null;
                    yoneticiMesaj.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    yoneticiMesaj.GuncelleTarih = DateTime.Now;
                    yoneticiMesaj.AktifMi = false;

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