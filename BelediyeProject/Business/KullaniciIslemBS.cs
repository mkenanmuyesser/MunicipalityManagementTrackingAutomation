using BelediyeProject.Entities;
using BelediyeProject.Helpers;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class KullaniciIslemBS
    {
        public static KullaniciIslemViewModel KullaniciGetir(Guid key)
        {
            Kullanici kullanici = null;

            using (DBEntities entities = new DBEntities())
            {
                kullanici = entities.Kullanicis.
                                                AsNoTracking().
                                                SingleOrDefault(p => p.KullaniciKey == key);
            }

            KullaniciIslemViewModel model = new KullaniciIslemViewModel
            {
                AktifMi = kullanici.AktifMi,
                RegisterId = kullanici.RegisterId,
                GuncelleKisiKey = kullanici.GuncelleKisiKey,
                GuncelleTarih = kullanici.GuncelleTarih,
                KayitKisiKey = kullanici.KayitKisiKey,
                KayitTarih = kullanici.KayitTarih,
                KullaniciAdi = kullanici.KullaniciAdi,
                KullaniciIsletimSistemiTipKey = kullanici.KullaniciIsletimSistemiTipKey,
                KullaniciKey = kullanici.KullaniciKey,
                KullaniciRolTipKey = kullanici.KullaniciRolTipKey,
                MacAdres = kullanici.MacAdres,
                Sifre = kullanici.Sifre,
            };
            return model;
        }

        public static List<Kullanici> PushNotificationKullanicilariGetir(int kullaniciIsletimSistemiTipKey)
        {
            List<Kullanici> kullanicilar = new List<Kullanici>();

            using (DBEntities entities = new DBEntities())
            {
                kullanicilar = entities.Kullanicis.AsNoTracking().
                                                   ToList().
                                                   Where(p => p.KullaniciIsletimSistemiTipKey == kullaniciIsletimSistemiTipKey).
                                                   ToList();
            }

            return kullanicilar;
        }

        public static List<Kullanici> KullaniciDetayliGetir(params int[] kullaniciRolTipleri)
        {
            List<Kullanici> kullanicilar = new List<Kullanici>();

            using (DBEntities entities = new DBEntities())
            {
                kullanicilar = entities.Kullanicis.
                                                   AsNoTracking().
                                                   Include("tt_KullaniciRolTip").
                                                   Include("tt_KullaniciIsletimSistemiTip").
                                                   ToList().
                                                   Where(p => kullaniciRolTipleri.Contains((p.KullaniciRolTipKey))).
                                                   ToList();
            }

            return kullanicilar;
        }

        public static List<tt_KullaniciRolTip> KullaniciRolTipGetir()
        {
            List<tt_KullaniciRolTip> kullaniciRolTipList = null;

            using (DBEntities entities = new DBEntities())
            {
                kullaniciRolTipList = entities.tt_KullaniciRolTip.
                                                                  AsNoTracking().
                                                                  Where(p => p.AktifMi &&
                                                                             !(p.KullaniciRolTipKey == 1 ||
                                                                               p.KullaniciRolTipKey == 3)).
                                                                  ToList();
            }

            return kullaniciRolTipList;
        }

        public static List<DosyaGonderilecekBirimTipData> DosyaGonderilecekBirimTipGetir()
        {
            List<DosyaGonderilecekBirimTipData> dosyaGonderilecekBirimTipData = new List<DosyaGonderilecekBirimTipData>();

            using (DBEntities entities = new DBEntities())
            {
                entities.tt_DosyaGonderilecekBirimTip.
                                                      AsNoTracking().
                                                      Where(p => p.AktifMi).
                                                      ToList().
                                                      ForEach(p => dosyaGonderilecekBirimTipData.Add(new DosyaGonderilecekBirimTipData
                                                      {
                                                          DosyaGonderilecekBirimTipAdi = p.DosyaGonderilecekBirimTipAdi,
                                                          DosyaGonderilecekBirimTipKey = p.DosyaGonderilecekBirimTipKey,
                                                          SecildiMi = false,
                                                      }));
            }

            return dosyaGonderilecekBirimTipData;
        }

        public static List<DosyaGonderilecekBirimTipData> SecilenDosyaGonderilecekBirimTipGetir(Guid key)
        {
            List<DosyaGonderilecekBirimTipData> secilenDosyaGonderilecekBirimTip = new List<DosyaGonderilecekBirimTipData>();

            using (DBEntities entities = new DBEntities())
            {
                entities.KullaniciBirims.
                                         AsNoTracking().
                                         Include("tt_DosyaGonderilecekBirimTip").
                                         Where(p => p.AktifMi &&
                                                    p.KullaniciKey == key).
                                         ToList().
                                         ForEach(p => secilenDosyaGonderilecekBirimTip.Add(new DosyaGonderilecekBirimTipData
                                         {
                                             DosyaGonderilecekBirimTipAdi = p.tt_DosyaGonderilecekBirimTip.DosyaGonderilecekBirimTipAdi,
                                             DosyaGonderilecekBirimTipKey = p.DosyaGonderilecekBirimTipKey,
                                             SecildiMi = true,
                                         }));
            }

            return secilenDosyaGonderilecekBirimTip;
        }

        public static bool KullaniciSil(Guid key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Kullanici kullanici = entities.Kullanicis.Single(p => p.KullaniciKey == key);

                    #region validation

                    //eğer silinen admin kullanıcısı ise ve tek admin kaldıysa silinemez
                    if (kullanici.KullaniciRolTipKey == 2)
                    {
                        if (entities.Kullanicis.Where(p => p.KullaniciRolTipKey == 2).Count() <= 1)
                        {
                            return false;
                        }
                    }

                    #endregion

                    kullanici.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    kullanici.GuncelleTarih = DateTime.Now;
                    kullanici.AktifMi = false;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool KullaniciKaydetGuncelle(KullaniciIslemViewModel viewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Kullanici kullanici = null;

                    if (viewModel.KullaniciKey == Guid.Empty)
                    {
                        Guid kullaniciKey = Guid.NewGuid();
                        kullanici = new Kullanici
                        {
                            KullaniciKey = kullaniciKey,
                            KullaniciRolTipKey = viewModel.KullaniciRolTipKey,
                            KullaniciIsletimSistemiTipKey = null,
                            KullaniciAdi = viewModel.KullaniciAdi,
                            Sifre = CryptoHelper.Sifrele(viewModel.Sifre),
                            RegisterId = null,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = viewModel.AktifMi
                        };
                        entities.Kullanicis.Add(kullanici);
                    }
                    else
                    {
                        kullanici = entities.Kullanicis.Single(p => p.KullaniciKey == viewModel.KullaniciKey);
                        kullanici.KullaniciRolTipKey = viewModel.KullaniciRolTipKey;
                        kullanici.KullaniciAdi = viewModel.KullaniciAdi;
                        kullanici.Sifre = CryptoHelper.Sifrele(viewModel.Sifre);

                        kullanici.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        kullanici.GuncelleTarih = DateTime.Now;
                        kullanici.AktifMi = viewModel.AktifMi;
                    }

                    entities.SaveChanges();

                    if (viewModel.KullaniciRolTipKey == 5 && viewModel.DosyaGonderilecekBirimTipKeys.Any())
                    {
                        //önceki birimleri pasife al
                        var oncekiBirimler = entities.KullaniciBirims.
                                                                      Where(p => p.KullaniciKey == kullanici.KullaniciKey);
                        foreach (var oncekiBirim in oncekiBirimler)
                        {
                            oncekiBirim.GuncelleKisiKey = kullaniciData.KullaniciKey;
                            oncekiBirim.GuncelleTarih = DateTime.Now;
                            oncekiBirim.AktifMi = viewModel.AktifMi;
                        }

                        foreach (var dosyaGonderilecekBirimTipKey in viewModel.DosyaGonderilecekBirimTipKeys)
                        {
                            var kullaniciBirim = new KullaniciBirim
                            {
                                KullaniciKey = kullanici.KullaniciKey,
                                DosyaGonderilecekBirimTipKey = Convert.ToInt32(dosyaGonderilecekBirimTipKey),

                                KayitKisiKey = kullaniciData.KullaniciKey,
                                KayitTarih = DateTime.Now,
                                GuncelleKisiKey = kullaniciData.KullaniciKey,
                                GuncelleTarih = DateTime.Now,
                                AktifMi = true,
                            };
                            entities.KullaniciBirims.Add(kullaniciBirim);
                        }

                        entities.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool KullaniciAktif(Guid key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    Kullanici kullanici = entities.Kullanicis.Single(p => p.KullaniciKey == key);

                    kullanici.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    kullanici.GuncelleTarih = DateTime.Now;
                    kullanici.AktifMi = true;

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