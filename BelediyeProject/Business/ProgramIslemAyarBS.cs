using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class ProgramIslemAyarBS
    {
        public static ProgramIslemAyarViewModel ProgramAyarGetir()
        {
            ProgramIslemAyarViewModel programIslemAyarViewModel = new ProgramIslemAyarViewModel();
            using (DBEntities entities = new DBEntities())
            {
                ProgramAyar programAyar = entities.ProgramAyars.AsNoTracking().
                                                                SingleOrDefault();
                programIslemAyarViewModel.Baslik = programAyar.Baslik;
                programIslemAyarViewModel.Kisaltma = programAyar.Kisaltma;
                programIslemAyarViewModel.ResimUrl = programAyar.ResimUrl;
                programIslemAyarViewModel.ServerAdres = programAyar.ServerAdres;
                programIslemAyarViewModel.SayfaAdres = programAyar.SayfaAdres;
                programIslemAyarViewModel.GoogleApiKey = programAyar.GoogleApiKey;
                programIslemAyarViewModel.AppleApiKey = programAyar.AppleApiKey;
                programIslemAyarViewModel.AppleSertifika = programAyar.AppleSertifika;
                programIslemAyarViewModel.KeyCode = programAyar.KeyCode;
            }

            return programIslemAyarViewModel;
        }

        public static bool ProgramAyarGuncelle(ProgramIslemAyarViewModel viewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    ProgramAyar programAyar = entities.ProgramAyars.Single();
                    programAyar.Baslik = viewModel.Baslik;
                    programAyar.Kisaltma = viewModel.Kisaltma;
                    programAyar.ServerAdres = viewModel.ServerAdres;
                    programAyar.SayfaAdres = viewModel.SayfaAdres;
                    programAyar.KeyCode = viewModel.KeyCode;
                    programAyar.GoogleApiKey = viewModel.GoogleApiKey;
                    programAyar.AppleApiKey = viewModel.AppleApiKey;

                    programAyar.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    programAyar.GuncelleTarih = DateTime.Now;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ProgramAyarSertifikaEkle(HttpPostedFileBase dosya, string dosyaYolu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                if (dosya != null && dosya.ContentLength > 0 )
                {
                    var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                    var path = Path.Combine(dosyaYolu, dosyaAdi);
                    dosya.SaveAs(path);

                    using (DBEntities entities = new DBEntities())
                    {
                        ProgramAyar programAyar = entities.ProgramAyars.Single();

                        programAyar.AppleSertifika = dosyaAdi;
                        programAyar.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        programAyar.GuncelleTarih = DateTime.Now;

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

        public static bool ProgramAyarSertifikaSil()
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    ProgramAyar programAyar = entities.ProgramAyars.Single();

                    programAyar.AppleSertifika = null;
                    programAyar.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    programAyar.GuncelleTarih = DateTime.Now;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ProgramAyarResimEkle(HttpPostedFileBase dosya, string dosyaYolu)
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
                        ProgramAyar programAyar = entities.ProgramAyars.Single();

                        programAyar.ResimUrl = dosyaAdi;
                        programAyar.GuncelleKisiKey = kullaniciData.KullaniciKey;
                        programAyar.GuncelleTarih = DateTime.Now;

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

        public static bool ProgramAyarResimSil()
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            try
            {
                using (DBEntities entities = new DBEntities())
                {
                    ProgramAyar programAyar = entities.ProgramAyars.Single();

                    programAyar.ResimUrl = null;
                    programAyar.GuncelleKisiKey = kullaniciData.KullaniciKey;
                    programAyar.GuncelleTarih = DateTime.Now;

                    entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GoogleApiKeyGetir()
        {
            string googleApiKey = "";
            using (DBEntities entities = new DBEntities())
            {
                ProgramAyar programAyar = entities.ProgramAyars.Single();
                googleApiKey = programAyar.GoogleApiKey;
            }

            return googleApiKey;
        }

        public static string AppleApiKeyGetir()
        {
            string appleApiKey = "";
            using (DBEntities entities = new DBEntities())
            {
                ProgramAyar programAyar = entities.ProgramAyars.Single();
                appleApiKey = programAyar.AppleApiKey;
            }

            return appleApiKey;
        }
    }
}