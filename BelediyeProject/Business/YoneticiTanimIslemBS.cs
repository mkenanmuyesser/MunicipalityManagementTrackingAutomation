using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class YoneticiTanimIslemBS
    {
        public static YoneticiTanimIslemViewModel YoneticiGetir()
        {
            YoneticiTanimIslemViewModel yoneticiTanimIslemViewModel = new YoneticiTanimIslemViewModel();
            using (DBEntities entities = new DBEntities())
            {
                Yonetici yonetici = entities.Yoneticis.
                                                        AsNoTracking().
                                                        Include("YoneticiResims").
                                                        SingleOrDefault();
                yoneticiTanimIslemViewModel.AdiSoyadi = yonetici.AdiSoyadi;
                yoneticiTanimIslemViewModel.Aciklama = yonetici.Aciklama;
                yoneticiTanimIslemViewModel.YoneticiResims = yonetici.YoneticiResims.Where(p => p.AktifMi).ToList();
            }

            return yoneticiTanimIslemViewModel;
        }

        public static bool YoneticiGuncelle(YoneticiTanimIslemViewModel yoneticiTanimIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Yonetici yonetici = entities.Yoneticis.Single();
                    yonetici.AdiSoyadi = yoneticiTanimIslemViewModel.AdiSoyadi;
                    yonetici.Aciklama = yoneticiTanimIslemViewModel.Aciklama;

                    yonetici.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    yonetici.GuncelleTarih = DateTime.Now;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool YoneticiResimEkle(HttpPostedFileBase dosya, string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                {
                    var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                    var path = Path.Combine(dosyaYolu, dosyaAdi);
                    dosya.SaveAs(path);

                    using (DBEntities entities = new DBEntities())
                    {
                        Yonetici yonetici = entities.Yoneticis.Single();
 
                        YoneticiResim yoneticiResim = new YoneticiResim
                        {
                            YoneticiKey = yonetici.YoneticiKey,
                            DosyaYolu = dosyaAdi,
                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };
                        entities.YoneticiResims.Add(yoneticiResim);

                        entities.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool YoneticiResimsil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    YoneticiResim yoneticiResim = entities.YoneticiResims.Single(p => p.YoneticiResimKey == key);

                    yoneticiResim.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    yoneticiResim.GuncelleTarih = DateTime.Now;
                    yoneticiResim.AktifMi = false;

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