using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class DergiIslemBS
    {
        public static DergiIslemViewModel DergiGetir(int key)
        {
            DergiIslemViewModel dergiIslemViewModel = new DergiIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var Dergi = entities.Dergis.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.DergiKey == key);

                dergiIslemViewModel.DergiKey = Dergi.DergiKey;
                dergiIslemViewModel.Adi = Dergi.Adi;
                dergiIslemViewModel.DosyaYolu = Dergi.DosyaYolu;
                dergiIslemViewModel.AktifMi = Dergi.AktifMi;
            }

            return dergiIslemViewModel;
        }

        public static List<Dergi> DergiListGetir()
        {

            List<Dergi> dergiler = new List<Dergi>();

            using (DBEntities entities = new DBEntities())
            {
                dergiler = entities.Dergis.
                                            AsNoTracking().
                                            ToList().
                                            OrderByDescending(p => p.DergiKey).
                                            ToList();
            }

            return dergiler;
        }

        public static bool DergiSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Dergi dergi = entities.Dergis.Single(p => p.DergiKey == key);

                    #region validation                   

                    #endregion

                    dergi.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    dergi.GuncelleTarih = DateTime.Now;
                    dergi.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DergiKaydetGuncelle(DergiIslemViewModel dergiIslemViewModel, string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Dergi dergi = null;

                    if (dergiIslemViewModel.DergiKey == 0 || dergiIslemViewModel.DergiKey == -1)
                    {
                        dergi = new Dergi
                        {
                            Adi = dergiIslemViewModel.Adi,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = dergiIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = dergiIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            dergi.DosyaYolu = dosyaAdi;
                        }

                        entities.Dergis.Add(dergi);

                    }
                    else
                    {
                        dergi = entities.Dergis.Single(p => p.DergiKey == dergiIslemViewModel.DergiKey);
                        dergi.Adi = dergiIslemViewModel.Adi;

                        HttpPostedFileBase dosya = dergiIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            dergi.DosyaYolu = dosyaAdi;
                        }

                        dergi.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        dergi.GuncelleTarih = DateTime.Now;
                        dergi.AktifMi = dergiIslemViewModel.AktifMi;
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

        public static bool DergiResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Dergi dergi = entities.Dergis.Single(p => p.DergiKey == key);

                    dergi.DosyaYolu = null;
                    dergi.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    dergi.GuncelleTarih = DateTime.Now;
                    dergi.AktifMi = false;

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