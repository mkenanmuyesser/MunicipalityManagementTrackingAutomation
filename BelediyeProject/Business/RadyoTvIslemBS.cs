using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class RadyoTvIslemBS
    {
        public static RadyoTvIslemViewModel RadyoTvGetir(int key)
        {
            RadyoTvIslemViewModel radyoTvIslemViewModel = new RadyoTvIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var radyoTv = entities.RadyoTvs.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.RadyoTvKey == key);

                radyoTvIslemViewModel.RadyoTvKey = radyoTv.RadyoTvKey;
                radyoTvIslemViewModel.RadyoTvTipKey = radyoTv.RadyoTvTipKey;
                radyoTvIslemViewModel.Adi = radyoTv.Adi;
                radyoTvIslemViewModel.Link = radyoTv.Link;
                radyoTvIslemViewModel.DosyaYolu = radyoTv.DosyaYolu;
                radyoTvIslemViewModel.AktifMi = radyoTv.AktifMi;
            }

            return radyoTvIslemViewModel;
        }

        public static List<RadyoTv> RadyoTvListGetir()
        {

            List<RadyoTv> radyoTvler = new List<RadyoTv>();

            using (DBEntities entities = new DBEntities())
            {
                radyoTvler = entities.RadyoTvs.
                                      Include("tt_RadyoTvTip").
                                      AsNoTracking().
                                      ToList();
            }

            return radyoTvler;
        }

        public static List<tt_RadyoTvTip> RadyoTvTipGetir()
        {
            List<tt_RadyoTvTip> radyoTvTipler = new List<tt_RadyoTvTip>();

            using (DBEntities entities = new DBEntities())
            {
                radyoTvTipler = entities.tt_RadyoTvTip.
                                      AsNoTracking().
                                      ToList();
            }

            return radyoTvTipler;
        }

        public static bool RadyoTvSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    RadyoTv radyoTv = entities.RadyoTvs.Single(p => p.RadyoTvKey == key);

                    #region validation                   

                    #endregion

                    radyoTv.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    radyoTv.GuncelleTarih = DateTime.Now;
                    radyoTv.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool RadyoTvKaydetGuncelle(RadyoTvIslemViewModel radyoTvIslemViewModel, string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    RadyoTv radyoTv = null;

                    if (radyoTvIslemViewModel.RadyoTvKey == 0 || radyoTvIslemViewModel.RadyoTvKey == -1)
                    {
                        radyoTv = new RadyoTv
                        {
                            RadyoTvTipKey = radyoTvIslemViewModel.RadyoTvTipKey,
                            Adi = radyoTvIslemViewModel.Adi,
                            Link = radyoTvIslemViewModel.Link,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = radyoTvIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = radyoTvIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            radyoTv.DosyaYolu = dosyaAdi;
                        }

                        entities.RadyoTvs.Add(radyoTv);

                    }
                    else
                    {
                        radyoTv = entities.RadyoTvs.Single(p => p.RadyoTvKey == radyoTvIslemViewModel.RadyoTvKey);
                        radyoTv.RadyoTvTipKey = radyoTvIslemViewModel.RadyoTvTipKey;
                        radyoTv.Adi = radyoTvIslemViewModel.Adi;
                        radyoTv.Link = radyoTvIslemViewModel.Link;

                        HttpPostedFileBase dosya = radyoTvIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            radyoTv.DosyaYolu = dosyaAdi;
                        }

                        radyoTv.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        radyoTv.GuncelleTarih = DateTime.Now;
                        radyoTv.AktifMi = radyoTvIslemViewModel.AktifMi;
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

        public static bool RadyoTvResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    RadyoTv radyoTv = entities.RadyoTvs.Single(p => p.RadyoTvKey == key);

                    radyoTv.DosyaYolu = null;
                    radyoTv.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    radyoTv.GuncelleTarih = DateTime.Now;
                    radyoTv.AktifMi = false;

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