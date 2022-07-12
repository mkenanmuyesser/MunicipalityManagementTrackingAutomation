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
    public class CenazeIslemBS
    {
        public static CenazeIslemViewModel CenazeGetir(int key)
        {
            CenazeIslemViewModel cenazeIslemViewModel = new CenazeIslemViewModel();

            using (DBEntities entities = new DBEntities())
            {
                var cenaze = entities.Cenazes.
                                            AsNoTracking().
                                            SingleOrDefault(p => p.CenazeKey == key);

                cenazeIslemViewModel.CenazeKey = cenaze.CenazeKey;
                cenazeIslemViewModel.AdiSoyadi = cenaze.AdiSoyadi;
                cenazeIslemViewModel.BabaAdi = cenaze.BabaAdi;
                cenazeIslemViewModel.Tarih = cenaze.Tarih;
                cenazeIslemViewModel.DogumYeri = cenaze.DogumYeri;
                cenazeIslemViewModel.Iletisim = cenaze.Iletisim;
                cenazeIslemViewModel.Aciklama = cenaze.Aciklama;

                cenazeIslemViewModel.AktifMi = cenaze.AktifMi;
            }

            return cenazeIslemViewModel;
        }

        public static List<Cenaze> CenazeListGetir()
        {

            List<Cenaze> cenazeler = new List<Cenaze>();

            using (DBEntities entities = new DBEntities())
            {
                cenazeler = entities.Cenazes.
                                     AsNoTracking().
                                     OrderByDescending(p => p.Tarih).
                                     ToList();
            }

            return cenazeler;
        }

        public static bool CenazeSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Cenaze cenaze = entities.Cenazes.Single(p => p.CenazeKey == key);

                    #region validation                   

                    #endregion

                    cenaze.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    cenaze.GuncelleTarih = DateTime.Now;
                    cenaze.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CenazeKaydetGuncelle(CenazeIslemViewModel cenazeIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Cenaze cenaze = null;

                    if (cenazeIslemViewModel.CenazeKey == 0 || cenazeIslemViewModel.CenazeKey == -1)
                    {
                        cenaze = new Cenaze
                        {
                            CenazeKey = cenazeIslemViewModel.CenazeKey,
                            AdiSoyadi = cenazeIslemViewModel.AdiSoyadi,
                            BabaAdi = cenazeIslemViewModel.BabaAdi,
                            Tarih = cenazeIslemViewModel.Tarih,
                            DogumYeri = cenazeIslemViewModel.DogumYeri,
                            Iletisim = cenazeIslemViewModel.Iletisim,
                            Aciklama = cenazeIslemViewModel.Aciklama,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = cenazeIslemViewModel.AktifMi
                        };
                        entities.Cenazes.Add(cenaze);

                    }
                    else
                    {
                        cenaze = entities.Cenazes.Single(p => p.CenazeKey == cenazeIslemViewModel.CenazeKey);
                        cenaze.AdiSoyadi = cenazeIslemViewModel.AdiSoyadi;
                        cenaze.BabaAdi = cenazeIslemViewModel.BabaAdi;
                        cenaze.Tarih = cenazeIslemViewModel.Tarih;
                        cenaze.DogumYeri = cenazeIslemViewModel.DogumYeri;
                        cenaze.Iletisim = cenazeIslemViewModel.Iletisim;
                        cenaze.Aciklama = cenazeIslemViewModel.Aciklama;

                        cenaze.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        cenaze.GuncelleTarih = DateTime.Now;
                        cenaze.AktifMi = cenazeIslemViewModel.AktifMi;
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

    }
}