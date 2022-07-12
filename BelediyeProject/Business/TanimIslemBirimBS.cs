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
    public class TanimIslemBirimBS
    {
        public static TanimIslemBirimViewModel BirimTipGetir(int key)
        {
            tt_DosyaGonderilecekBirimTip birimTip = null;

            using (DBEntities entities = new DBEntities())
            {
                birimTip = entities.tt_DosyaGonderilecekBirimTip.
                                                AsNoTracking().
                                                SingleOrDefault(p => p.DosyaGonderilecekBirimTipKey == key);
            }

            TanimIslemBirimViewModel model = new TanimIslemBirimViewModel
            {
                AktifMi = birimTip.AktifMi,
                DosyaGonderilecekBirimTipAdi = birimTip.DosyaGonderilecekBirimTipAdi,
                DosyaGonderilecekBirimTipKey = birimTip.DosyaGonderilecekBirimTipKey,
            };
            return model;
        }

        public static List<tt_DosyaGonderilecekBirimTip> BirimTipleriGetir()
        {
            List<tt_DosyaGonderilecekBirimTip> birimTipleri = new List<tt_DosyaGonderilecekBirimTip>();

            using (DBEntities entities = new DBEntities())
            {
                birimTipleri = entities.tt_DosyaGonderilecekBirimTip.
                                                   AsNoTracking().
                                                   ToList();

            }

            return birimTipleri;
        }

        public static bool BirimTipSil(int key)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    tt_DosyaGonderilecekBirimTip birimTip = entities.tt_DosyaGonderilecekBirimTip.Single(p => p.DosyaGonderilecekBirimTipKey == key);

                    #region validation

                    #endregion

                    entities.tt_DosyaGonderilecekBirimTip.Remove(birimTip);

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool BirimTipKaydetGuncelle(TanimIslemBirimViewModel viewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    tt_DosyaGonderilecekBirimTip birimTip = null;

                    if (viewModel.DosyaGonderilecekBirimTipKey == 0)
                    {
                        birimTip = new tt_DosyaGonderilecekBirimTip
                        {
                            DosyaGonderilecekBirimTipAdi= viewModel.DosyaGonderilecekBirimTipAdi,

                            KayitKisiKey = kullaniciData.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciData.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = viewModel.AktifMi
                        };
                        entities.tt_DosyaGonderilecekBirimTip.Add(birimTip);
                    }
                    else
                    {
                        birimTip = entities.tt_DosyaGonderilecekBirimTip.Single(p => p.DosyaGonderilecekBirimTipKey == viewModel.DosyaGonderilecekBirimTipKey);
                        birimTip.DosyaGonderilecekBirimTipAdi = viewModel.DosyaGonderilecekBirimTipAdi;

                        birimTip.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        birimTip.GuncelleTarih = DateTime.Now;
                        birimTip.AktifMi = viewModel.AktifMi;
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