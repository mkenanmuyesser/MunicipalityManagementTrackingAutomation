using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class ReklamIslemBS
    {
        public static ReklamIslemViewModel ReklamGetir(int key)
        {
            ReklamIslemViewModel reklamIslemViewModel = new ReklamIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var Reklam = entities.Reklams.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.ReklamKey == key);

                reklamIslemViewModel.ReklamKey = Reklam.ReklamKey;
                reklamIslemViewModel.Link = Reklam.Link;
                reklamIslemViewModel.DosyaYolu = Reklam.DosyaYolu;
                reklamIslemViewModel.BaslangicTarihi = Reklam.BaslangicTarihi;
                reklamIslemViewModel.BitisTarihi = Reklam.BitisTarihi;
                reklamIslemViewModel.AktifMi = Reklam.AktifMi;
            }

            return reklamIslemViewModel;
        }

        public static List<Reklam> ReklamListGetir()
        {

            List<Reklam> reklamlar = new List<Reklam>();

            using (DBEntities entities = new DBEntities())
            {
                reklamlar = entities.Reklams.
                                            AsNoTracking().
                                            ToList();
            }

            return reklamlar;
        }

        public static bool ReklamSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Reklam reklam = entities.Reklams.Single(p => p.ReklamKey == key);

                    #region validation                   

                    #endregion

                    reklam.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    reklam.GuncelleTarih = DateTime.Now;
                    reklam.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ReklamKaydetGuncelle(ReklamIslemViewModel reklamIslemViewModel, string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Reklam reklam = null;

                    if (reklamIslemViewModel.ReklamKey == 0 || reklamIslemViewModel.ReklamKey == -1)
                    {
                        reklam = new Reklam
                        {
                            Link = reklamIslemViewModel.Link,
                            BaslangicTarihi = reklamIslemViewModel.BaslangicTarihi,
                            BitisTarihi = reklamIslemViewModel.BitisTarihi,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = reklamIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = reklamIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            reklam.DosyaYolu = dosyaAdi;
                        }

                        entities.Reklams.Add(reklam);

                    }
                    else
                    {
                        reklam = entities.Reklams.Single(p => p.ReklamKey == reklamIslemViewModel.ReklamKey);
                        reklam.Link = reklamIslemViewModel.Link;
                        reklam.BaslangicTarihi = reklamIslemViewModel.BaslangicTarihi;
                        reklam.BitisTarihi = reklamIslemViewModel.BitisTarihi;

                        HttpPostedFileBase dosya = reklamIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            reklam.DosyaYolu = dosyaAdi;
                        }

                        reklam.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        reklam.GuncelleTarih = DateTime.Now;
                        reklam.AktifMi = reklamIslemViewModel.AktifMi;
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

        public static bool ReklamResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Reklam reklam = entities.Reklams.Single(p => p.ReklamKey == key);

                    reklam.DosyaYolu = null;
                    reklam.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    reklam.GuncelleTarih = DateTime.Now;
                    reklam.AktifMi = false;

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