using BelediyeProject.Entities;
using BelediyeProject.Models;
using PushSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class DuyuruIslemBS
    {
        public static DuyuruIslemViewModel DuyuruGetir(int key)
        {
            DuyuruIslemViewModel duyuruIslemViewModel = new DuyuruIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var duyuru = entities.Duyurus.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.DuyuruKey == key);

                duyuruIslemViewModel.DuyuruKey = duyuru.DuyuruKey;
                duyuruIslemViewModel.Baslik = duyuru.Baslik;
                duyuruIslemViewModel.Ozet = duyuru.Ozet;
                duyuruIslemViewModel.Tarih = duyuru.Tarih;
                duyuruIslemViewModel.AktifMi = duyuru.AktifMi;
            }

            return duyuruIslemViewModel;
        }

        public static List<Duyuru> DuyuruListGetir()
        {

            List<Duyuru> duyurular = new List<Duyuru>();

            using (DBEntities entities = new DBEntities())
            {
                duyurular = entities.Duyurus.
                                     AsNoTracking().
                                     OrderByDescending(p => p.Tarih).
                                     ToList();
            }

            return duyurular;
        }

        public static bool DuyuruSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Duyuru duyuru = entities.Duyurus.Single(p => p.DuyuruKey == key);

                    #region validation                   

                    #endregion

                    duyuru.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    duyuru.GuncelleTarih = DateTime.Now;
                    duyuru.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DuyuruKaydetGuncelle(DuyuruIslemViewModel duyuruIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Duyuru duyuru = null;

                    if (duyuruIslemViewModel.DuyuruKey == 0 || duyuruIslemViewModel.DuyuruKey == -1)
                    {
                        duyuru = new Duyuru
                        {
                            Baslik = duyuruIslemViewModel.Baslik,
                            Ozet = duyuruIslemViewModel.Ozet,
                            Tarih = DateTime.Now,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = duyuruIslemViewModel.AktifMi
                        };
                        entities.Duyurus.Add(duyuru);

                    }
                    else
                    {
                        duyuru = entities.Duyurus.Single(p => p.DuyuruKey == duyuruIslemViewModel.DuyuruKey);
                        duyuru.Baslik = duyuruIslemViewModel.Baslik;
                        duyuru.Ozet = duyuruIslemViewModel.Ozet;
                        duyuru.Tarih = DateTime.Now;

                        duyuru.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        duyuru.GuncelleTarih = DateTime.Now;
                        duyuru.AktifMi = duyuruIslemViewModel.AktifMi;
                    }

                    entities.SaveChanges();

                    //push notification gönder
                    if (duyuru.AktifMi)
                    {
                        var androidKullanicilari = KullaniciIslemBS.PushNotificationKullanicilariGetir(1);

                        for (int i = 0; i * 1000 < androidKullanicilari.Count(); i++)
                        {
                            MobileBS.PushNotificationToAndroid(androidKullanicilari.Skip(i * 1000).Take(1000).Select(p => p.RegisterId), duyuru.DuyuruKey.ToString(), duyuru.Baslik);
                        }

                        var appleKullanicilari = KullaniciIslemBS.PushNotificationKullanicilariGetir(2);
                        foreach (Kullanici kullanici in appleKullanicilari)
                        {
                            MobileBS.PushNotificationToIos(kullanici.DeviceToken, duyuru.DuyuruKey.ToString(), duyuru.Baslik);
                        }
                    }

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