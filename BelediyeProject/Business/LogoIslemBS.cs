using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class LogoIslemBS
    {
        public static LogoIslemViewModel LogoGetir()
        {
            LogoIslemViewModel logoIslemViewModel = new LogoIslemViewModel();
            using (DBEntities entities = new DBEntities())
            {
                Logo logo = entities.Logoes.
                                     AsNoTracking().
                                     Where(p => p.AktifMi).
                                     ToList().
                                     LastOrDefault();

                logoIslemViewModel.LogoKey = logo == null ? 0 : logo.LogoKey;
                logoIslemViewModel.DosyaYolu = logo == null ? null : logo.DosyaYolu;
            }

            return logoIslemViewModel;
        }

        public static bool LogoResimEkle(HttpPostedFileBase dosya, string dosyaYolu)
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
                        Logo logo = new Logo
                        {
                            DosyaYolu = dosyaAdi,
                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };
                        entities.Logoes.Add(logo);

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

        public static bool LogoResimsil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Logo logo = entities.Logoes.SingleOrDefault(p => p.LogoKey == key);                                                                                                                       
                    logo.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    logo.GuncelleTarih = DateTime.Now;
                    logo.AktifMi = false;

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