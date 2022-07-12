using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class DergiSayfaIslemBS
    {
        public static DergiSayfaIslemViewModel DergiSayfaGetir(int key)
        {
            DergiSayfaIslemViewModel dergiSayfaIslemViewModel = new DergiSayfaIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var Dergi = entities.DergiSayfas.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.DergiSayfaKey == key);

                dergiSayfaIslemViewModel.DergiSayfaKey = Dergi.DergiSayfaKey;
                dergiSayfaIslemViewModel.DergiKey = Dergi.DergiKey;
                dergiSayfaIslemViewModel.SayfaNo = Dergi.SayfaNo;
                dergiSayfaIslemViewModel.DosyaYolu = Dergi.DosyaYolu;
                dergiSayfaIslemViewModel.AktifMi = Dergi.AktifMi;
            }

            return dergiSayfaIslemViewModel;
        }

        public static List<DergiSayfa> DergiSayfaListGetir()
        {

            List<DergiSayfa> dergiSayfalar = new List<DergiSayfa>();

            using (DBEntities entities = new DBEntities())
            {
                dergiSayfalar = entities.DergiSayfas.
                                            AsNoTracking().
                                            Include("Dergi").
                                            ToList().
                                            OrderBy(p => p.SayfaNo).
                                            OrderByDescending(p => p.DergiKey).
                                            ToList();
            }

            return dergiSayfalar;
        }

        public static bool DergiSayfaSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    DergiSayfa dergiSayfa = entities.DergiSayfas.Single(p => p.DergiSayfaKey == key);

                    #region validation                   

                    #endregion

                    dergiSayfa.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    dergiSayfa.GuncelleTarih = DateTime.Now;
                    dergiSayfa.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DergiSayfaKaydetGuncelle(DergiSayfaIslemViewModel dergiSayfaIslemViewModel, string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    DergiSayfa dergiSayfa = null;

                    if (dergiSayfaIslemViewModel.DergiSayfaKey == 0 || dergiSayfaIslemViewModel.DergiSayfaKey == -1)
                    {
                        dergiSayfa = new DergiSayfa
                        {
                            DergiKey = dergiSayfaIslemViewModel.DergiKey,
                            SayfaNo = dergiSayfaIslemViewModel.SayfaNo,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = dergiSayfaIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = dergiSayfaIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            dergiSayfa.DosyaYolu = dosyaAdi;
                        }

                        entities.DergiSayfas.Add(dergiSayfa);

                    }
                    else
                    {
                        dergiSayfa = entities.DergiSayfas.Single(p => p.DergiSayfaKey == dergiSayfaIslemViewModel.DergiSayfaKey);
                        dergiSayfa.DergiKey = dergiSayfaIslemViewModel.DergiKey;
                        dergiSayfa.SayfaNo = dergiSayfaIslemViewModel.SayfaNo;

                        HttpPostedFileBase dosya = dergiSayfaIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            dergiSayfa.DosyaYolu = dosyaAdi;
                        }

                        dergiSayfa.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        dergiSayfa.GuncelleTarih = DateTime.Now;
                        dergiSayfa.AktifMi = dergiSayfaIslemViewModel.AktifMi;
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

        public static bool DergiSayfaResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    DergiSayfa dergiSayfa = entities.DergiSayfas.Single(p => p.DergiSayfaKey == key);

                    dergiSayfa.DosyaYolu = null;
                    dergiSayfa.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    dergiSayfa.GuncelleTarih = DateTime.Now;
                    dergiSayfa.AktifMi = false;

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