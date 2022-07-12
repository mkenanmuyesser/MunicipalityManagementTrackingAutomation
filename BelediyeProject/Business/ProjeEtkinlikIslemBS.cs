using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class ProjeEtkinlikIslemBS
    {
 
        public static ProjeEtkinlikIslemViewModel ProjeEtkinlikGetir(int key)
        {
            ProjeEtkinlikIslemViewModel ProjeEtkinlikIslemViewModel = new ProjeEtkinlikIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var ProjeEtkinlik = entities.ProjeEtkinliks.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.ProjeEtkinlikKey == key);

                ProjeEtkinlikIslemViewModel.ProjeEtkinlikKey = ProjeEtkinlik.ProjeEtkinlikKey;
                ProjeEtkinlikIslemViewModel.Baslik = ProjeEtkinlik.Baslik;
                ProjeEtkinlikIslemViewModel.Aciklama = ProjeEtkinlik.Aciklama;
                ProjeEtkinlikIslemViewModel.DosyaYolu = ProjeEtkinlik.DosyaYolu;
                ProjeEtkinlikIslemViewModel.Tarih = ProjeEtkinlik.Tarih;
                ProjeEtkinlikIslemViewModel.AktifMi = ProjeEtkinlik.AktifMi;
            }

            return ProjeEtkinlikIslemViewModel;
        }

        public static List<ProjeEtkinlik> ProjeEtkinlikListGetir()
        {

            List<ProjeEtkinlik> ProjeEtkinlik = new List<ProjeEtkinlik>();

            using (DBEntities entities = new DBEntities())
            {
                ProjeEtkinlik = entities.ProjeEtkinliks.
                                            AsNoTracking().
                                            OrderByDescending(p => p.Tarih).
                                            ToList();
            }

            return ProjeEtkinlik;
        }

        public static bool ProjeEtkinlikSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    ProjeEtkinlik ProjeEtkinlik = entities.ProjeEtkinliks.Single(p => p.ProjeEtkinlikKey == key);

                    #region validation                   

                    #endregion

                    ProjeEtkinlik.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    ProjeEtkinlik.GuncelleTarih = DateTime.Now;
                    ProjeEtkinlik.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ProjeEtkinlikKaydetGuncelle(ProjeEtkinlikIslemViewModel ProjeEtkinlikIslemViewModel,string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    ProjeEtkinlik ProjeEtkinlik = null;

                    if (ProjeEtkinlikIslemViewModel.ProjeEtkinlikKey == 0 || ProjeEtkinlikIslemViewModel.ProjeEtkinlikKey == -1)
                    {
                        ProjeEtkinlik = new ProjeEtkinlik
                        {
                            Baslik = ProjeEtkinlikIslemViewModel.Baslik,
                            Aciklama = ProjeEtkinlikIslemViewModel.Aciklama,
                            Tarih = ProjeEtkinlikIslemViewModel.Tarih,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = ProjeEtkinlikIslemViewModel.AktifMi
                        };

                        HttpPostedFileBase dosya = ProjeEtkinlikIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            ProjeEtkinlik.DosyaYolu = dosyaAdi;
                        }

                        entities.ProjeEtkinliks.Add(ProjeEtkinlik);

                    }
                    else
                    {
                        ProjeEtkinlik = entities.ProjeEtkinliks.Single(p => p.ProjeEtkinlikKey == ProjeEtkinlikIslemViewModel.ProjeEtkinlikKey);
                        ProjeEtkinlik.Baslik = ProjeEtkinlikIslemViewModel.Baslik;
                        ProjeEtkinlik.Aciklama = ProjeEtkinlikIslemViewModel.Aciklama;
                        ProjeEtkinlik.Tarih = ProjeEtkinlikIslemViewModel.Tarih;

                        HttpPostedFileBase dosya = ProjeEtkinlikIslemViewModel.ImageUpload;
                        if (dosya != null && dosya.ContentLength > 0 && (dosya.ContentType == "image/jpeg" || dosya.ContentType == "image/png"))
                        {
                            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var path = Path.Combine(dosyaYolu, dosyaAdi);
                            dosya.SaveAs(path);

                            ProjeEtkinlik.DosyaYolu = dosyaAdi;
                        }

                        ProjeEtkinlik.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        ProjeEtkinlik.GuncelleTarih = DateTime.Now;
                        ProjeEtkinlik.AktifMi = ProjeEtkinlikIslemViewModel.AktifMi;
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

        public static bool ProjeEtkinlikResimSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    ProjeEtkinlik ProjeEtkinlik = entities.ProjeEtkinliks.Single(p => p.ProjeEtkinlikKey == key);

                    ProjeEtkinlik.DosyaYolu = null;
                    ProjeEtkinlik.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    ProjeEtkinlik.GuncelleTarih = DateTime.Now;
                    ProjeEtkinlik.AktifMi = false;

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