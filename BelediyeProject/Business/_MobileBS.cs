using BelediyeProject.Entities;
using BelediyeProject.Helpers;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BelediyeProject.Business
{
    public class MobileBS
    {
        public static bool KullaniciKayit(MobileKullaniciData mobileKullaniciData)
        {
            using (DBEntities entities = new DBEntities())
            {
                try
                {
                    var kullanici = entities.Kullanicis.SingleOrDefault(p => p.RegisterId == mobileKullaniciData.RegisterId);
                    if (kullanici == null)
                    {
                        Guid kullaniciKey = Guid.NewGuid();
                        kullanici = new Kullanici
                        {
                            KullaniciKey = kullaniciKey,
                            KullaniciRolTipKey = 3,
                            KullaniciIsletimSistemiTipKey = mobileKullaniciData.KullaniciIsletimSistemiTipKey,
                            KullaniciAdi = mobileKullaniciData.KullaniciAdi,
                            Sifre = null,
                            MacAdres = mobileKullaniciData.MacAdres,
                            RegisterId = mobileKullaniciData.RegisterId,
                            DeviceToken = mobileKullaniciData.DeviceToken,
                            UserId=mobileKullaniciData.UserId,

                            KayitKisiKey = kullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };
                        entities.Kullanicis.Add(kullanici);

                        entities.SaveChanges();

                        return true;
                    }
                    else
                    {
                        kullanici.MacAdres = mobileKullaniciData.MacAdres;
                        kullanici.DeviceToken = mobileKullaniciData.DeviceToken;
                        kullanici.KullaniciAdi = mobileKullaniciData.KullaniciAdi;
                        kullanici.UserId = mobileKullaniciData.UserId;

                        kullanici.GuncelleTarih = DateTime.Now;

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

        public static bool DosyaKayit(MobileDosyaData mobileDosyaData)
        {
            using (DBEntities entities = new DBEntities())
            {
                try
                {
                    var kullanici = entities.Kullanicis.AsNoTracking().
                                                        SingleOrDefault(p =>
                                                                            p.AktifMi &&
                                                                            p.KullaniciRolTipKey == 3 &&
                                                                            p.RegisterId == mobileDosyaData.RegisterId);

                    if (kullanici == null)
                    {
                        return false;
                    }
                    else
                    {
                        Dosya dosya = new Dosya
                        {
                            KullaniciKey = kullanici.KullaniciKey,
                            DosyaAcilmaNedenTipKey = mobileDosyaData.DosyaAcilmaNedenTipKey,
                            DosyaDurumTipKey = 1,
                            DosyaGonderilecekBirimTipKey = mobileDosyaData.DosyaGonderilecekBirimTipKey == null || mobileDosyaData.DosyaGonderilecekBirimTipKey.Value == 99 ? 0 : mobileDosyaData.DosyaGonderilecekBirimTipKey.Value,
                            DosyaMesajTipKey = mobileDosyaData.DosyaMesajTipKey == null ? 99 : mobileDosyaData.DosyaMesajTipKey.Value,
                            DosyaKonumTipKey = mobileDosyaData.DosyaKonumTipKey == null ? null : (int?)mobileDosyaData.DosyaKonumTipKey.Value,
                            Mesaj = mobileDosyaData.Mesaj,
                            IpAdres = HttpContext.Current == null ? "" : HttpContext.Current.Request.UserHostAddress,
                            Enlem = mobileDosyaData.Enlem,
                            Boylam = mobileDosyaData.Boylam,
                            IslemZamani = DateTime.Now,
                            Adi = mobileDosyaData.Adi,
                            Soyadi = mobileDosyaData.Soyadi,
                            IrtibatNo = mobileDosyaData.IrtibatNo,

                            KayitKisiKey = kullanici.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullanici.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };
                        entities.Dosyas.Add(dosya);

                        if (mobileDosyaData.Resimler != null)
                        {
                            var resimler = mobileDosyaData.Resimler.Split('|');
                            foreach (var resim in resimler)
                            {
                                DosyaResim dosyaResim = new DosyaResim
                                {
                                    Dosya = dosya,
                                    DosyaYolu = resim,

                                    KayitKisiKey = kullanici.KullaniciKey,
                                    KayitTarih = DateTime.Now,
                                    GuncelleKisiKey = kullanici.KullaniciKey,
                                    GuncelleTarih = DateTime.Now,
                                    AktifMi = true,
                                };
                                entities.DosyaResims.Add(dosyaResim);
                            }
                        }

                        if (mobileDosyaData.Videolar != null)
                        {
                            var videolar = mobileDosyaData.Videolar.Split('|');
                            foreach (var video in videolar)
                            {
                                DosyaVideo dosyaVideo = new DosyaVideo
                                {
                                    Dosya = dosya,
                                    DosyaYolu = video,

                                    KayitKisiKey = kullanici.KullaniciKey,
                                    KayitTarih = DateTime.Now,
                                    GuncelleKisiKey = kullanici.KullaniciKey,
                                    GuncelleTarih = DateTime.Now,
                                    AktifMi = true,
                                };
                                entities.DosyaVideos.Add(dosyaVideo);
                            }
                        }

                        entities.SaveChanges();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool DosyaHashKayit(MobileDosyaHashData mobileDosyaHashData)
        {
            using (DBEntities entities = new DBEntities())
            {
                try
                {
                    var kullanici = entities.Kullanicis.AsNoTracking().
                                                        SingleOrDefault(p =>
                                                                            p.AktifMi &&
                                                                            p.KullaniciRolTipKey == 3 &&
                                                                            p.RegisterId == mobileDosyaHashData.RegisterId);

                    if (kullanici == null)
                    {
                        return false;
                    }
                    else
                    {
                        DosyaHash dosyaHash = new DosyaHash
                        {
                            KullaniciKey = kullanici.KullaniciKey,
                            IpAdres = HttpContext.Current == null ? "" : HttpContext.Current.Request.UserHostAddress,
                            Enlem = mobileDosyaHashData.Enlem,
                            Boylam = mobileDosyaHashData.Boylam,
                            HashBilgi = mobileDosyaHashData.HashBilgi,

                            KayitKisiKey = kullanici.KullaniciKey,
                            KayitTarih = DateTime.Now,
                            GuncelleKisiKey = kullanici.KullaniciKey,
                            GuncelleTarih = DateTime.Now,
                            AktifMi = true,
                        };
                        entities.DosyaHashes.Add(dosyaHash);

                        entities.SaveChanges();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static MobileDosyaDurumData[] DosyaDurum(string id)
        {
            List<MobileDosyaDurumData> mobileDosyaDurumData = new List<MobileDosyaDurumData>();
            using (DBEntities entities = new DBEntities())
            {

                var kullanici = entities.Kullanicis.
                                                    AsNoTracking().
                                                    SingleOrDefault(p =>
                                                                        p.AktifMi &&
                                                                        p.KullaniciRolTipKey == 3 &&
                                                                        p.RegisterId == id);
                if (kullanici != null)
                {
                    entities.Dosyas.
                                    AsNoTracking().
                                    Include("tt_DosyaAcilmaNedenTip").
                                    Include("tt_DosyaDurumTip").
                                    Include("tt_DosyaGonderilecekBirimTip").
                                    Include("tt_DosyaMesajTip").
                                    Where(p => p.AktifMi &&
                                               p.KullaniciKey == kullanici.KullaniciKey).
                                    ToList().
                                    ForEach(
                                             p =>
                                             mobileDosyaDurumData.Add(
                                             new MobileDosyaDurumData
                                             {
                                                 IslemZamani = p.IslemZamani,
                                                 DosyaAcilmaNedenTipAdi = p.tt_DosyaAcilmaNedenTip.DosyaAcilmaNedenTipAdi,
                                                 DosyaDurumTipAdi = p.tt_DosyaDurumTip.DosyaDurumTipAdi,
                                                 DosyaGonderilecekBirimTipAdi = p.tt_DosyaGonderilecekBirimTip == null ? "" : p.tt_DosyaGonderilecekBirimTip.DosyaGonderilecekBirimTipAdi,
                                                 DosyaMesajTipAdi = p.tt_DosyaMesajTip == null ? "" : p.tt_DosyaMesajTip.DosyaMesajTipAdi,
                                                 Mesaj = p.Mesaj
                                             })
                        );

                }
            }

            return mobileDosyaDurumData.ToArray();
        }

        public static MobileReklamData[] Reklam()
        {
            List<MobileReklamData> mobileReklamData = new List<MobileReklamData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.Reklams.AsNoTracking().
                                              Where(p => p.AktifMi &&
                                                       p.BaslangicTarihi <= DateTime.Now &&
                                                       p.BitisTarihi >= DateTime.Now).
                                              ToList().
                                               ForEach(
                                             p =>
                                             mobileReklamData.Add(
                                             new MobileReklamData
                                             {
                                                 Link = p.Link,
                                                 DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                             }));
            }

            return mobileReklamData.ToArray();
        }

        public static MobileYoneticiData Yonetici()
        {
            MobileYoneticiData mobileTanitimYoneticiData = new MobileYoneticiData();
            using (DBEntities entities = new DBEntities())
            {
                var tanitimBaskan = entities.Yoneticis.
                                             AsNoTracking().
                                             SingleOrDefault();
                if (tanitimBaskan != null)
                {
                    mobileTanitimYoneticiData.AdiSoyadi = tanitimBaskan.AdiSoyadi;
                    mobileTanitimYoneticiData.Aciklama = tanitimBaskan.Aciklama;
                }
            }

            return mobileTanitimYoneticiData;
        }

        public static MobileYoneticiMesajData[] YoneticiMesaj(string id)
        {
            List<MobileYoneticiMesajData> mobileYoneticiMesajData = new List<MobileYoneticiMesajData>();

            int sayac = 0;
            int baslangic = 0;
            int bitis = 0;
            if (id.Contains('-'))
            {
                var baslangicbitis = id.Split('-');

                baslangic = Convert.ToInt32(baslangicbitis[0]);
                bitis = Convert.ToInt32(baslangicbitis[1]);
            }
            else
            {
                sayac = Convert.ToInt32(id);
            }

            using (DBEntities entities = new DBEntities())
            {
                entities.YoneticiMesajs.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            OrderByDescending(p => p.Tarih).
                                            Skip(sayac == 0 ? baslangic : 0).
                                            Take(sayac == 0 ? bitis : sayac).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileYoneticiMesajData.Add(
                                                     new MobileYoneticiMesajData
                                                     {
                                                         Baslik = p.Baslik,
                                                         Aciklama = p.Aciklama,
                                                         Tarih = p.Tarih,
                                                         DosyaYolu = p.DosyaYolu
                                                     })
                    );
            }

            return mobileYoneticiMesajData.ToArray();
        }

        public static MobileLogoResimData YoneticiResim()
        {
            List<MobileLogoResimData> mobileTanitimYoneticiResimData = new List<MobileLogoResimData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.YoneticiResims.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileTanitimYoneticiResimData.Add(
                                                     new MobileLogoResimData
                                                     {
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileTanitimYoneticiResimData.FirstOrDefault();
        }

        public static MobileBannerData[] BannerResim()
        {
            List<MobileBannerData> mobileBannerResimData = new List<MobileBannerData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.Banners.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileBannerResimData.Add(
                                                     new MobileBannerData
                                                     {
                                                         Link = p.Link,
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileBannerResimData.ToArray();
        }

        public static MobileGaleriData[] GaleriResim()
        {
            List<MobileGaleriData> mobileGaleriResimData = new List<MobileGaleriData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.Galeris.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileGaleriResimData.Add(
                                                     new MobileGaleriData
                                                     {
                                                         Link = p.Link,
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileGaleriResimData.ToArray();
        }

        public static MobileLogoResimData LogoResim()
        {
            List<MobileLogoResimData> mobileLogoResimData = new List<MobileLogoResimData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.Logoes.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileLogoResimData.Add(
                                                     new MobileLogoResimData
                                                     {
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileLogoResimData.LastOrDefault();
        }

        public static MobileSayfaAdresData SayfaAdres()
        {
            MobileSayfaAdresData mobileAnaSayfaData = new MobileSayfaAdresData();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();

                mobileAnaSayfaData.SayfaAdres = programAyar.SayfaAdres;
            }

            return mobileAnaSayfaData;
        }

        public static MobileDuyuruData Duyuru(string id)
        {
            MobileDuyuruData mobileDuyuruData = new MobileDuyuruData();
            using (DBEntities entities = new DBEntities())
            {
                var data = entities.Duyurus.AsNoTracking().
                                                  ToList().
                                                  SingleOrDefault(p => p.AktifMi &&
                                                                       p.DuyuruKey == Convert.ToInt32(id));

                mobileDuyuruData.Baslik = data.Baslik;
                mobileDuyuruData.Ozet = data.Ozet;
                mobileDuyuruData.Tarih = data.Tarih;
            }

            return mobileDuyuruData;
        }

        public static MobileLimitData Limit()
        {
            MobileLimitData mobileLimitData = new MobileLimitData();
            mobileLimitData.LimitIzin = false;
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                var kullaniciSayisi = entities.Kullanicis.AsNoTracking().
                                                 Count(p => p.AktifMi &&
                                                 p.KullaniciIsletimSistemiTipKey != null);

                try
                {
                    int limit = Convert.ToInt32(CryptoHelper.Base64Decode(programAyar.Limit));

                    if (limit >= kullaniciSayisi)
                    {
                        mobileLimitData.LimitIzin = true;
                    }
                    else
                    {
                        mobileLimitData.LimitIzin = false;
                    }
                }
                catch (Exception ex)
                {

                    mobileLimitData.LimitIzin = false;
                }               
            }

            return mobileLimitData;
        }

        public static MobileDuyuruData[] Duyuru()
        {
            List<MobileDuyuruData> mobileDuyuruData = new List<MobileDuyuruData>();
            using (DBEntities entities = new DBEntities())
            {
                entities.Duyurus.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            OrderByDescending(p => p.Tarih).
                                            Take(10).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileDuyuruData.Add(
                                                     new MobileDuyuruData
                                                     {
                                                         Tarih = p.Tarih,
                                                         Baslik = p.Baslik,
                                                         Ozet = p.Ozet,
                                                     })
                    );
            }

            return mobileDuyuruData.ToArray();
        }

        public static MobileHaberData[] Haber(string id)
        {
            List<MobileHaberData> mobileHaberData = new List<MobileHaberData>();

            int sayac = 0;
            int baslangic = 0;
            int bitis = 0;
            if (id.Contains('-'))
            {
                var baslangicbitis = id.Split('-');

                baslangic = Convert.ToInt32(baslangicbitis[0]);
                bitis = Convert.ToInt32(baslangicbitis[1]);
            }
            else
            {
                sayac = Convert.ToInt32(id);
            }

            using (DBEntities entities = new DBEntities())
            {
                entities.Habers.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            OrderByDescending(p => p.Tarih).
                                            Skip(sayac == 0 ? baslangic : 0).
                                            Take(sayac == 0 ? bitis : sayac).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileHaberData.Add(
                                                     new MobileHaberData
                                                     {
                                                         Baslik = p.Baslik,
                                                         Aciklama = p.Aciklama,
                                                         Tarih = p.Tarih,
                                                         DosyaYolu = p.DosyaYolu
                                                     })
                    );
            }

            return mobileHaberData.ToArray();
        }

        public static MobileProjeEtkinlikData[] ProjeEtkinlik(string id)
        {
            List<MobileProjeEtkinlikData> mobileProjeEtkinlikData = new List<MobileProjeEtkinlikData>();

            int sayac = 0;
            int baslangic = 0;
            int bitis = 0;
            if (id.Contains('-'))
            {
                var baslangicbitis = id.Split('-');

                baslangic = Convert.ToInt32(baslangicbitis[0]);
                bitis = Convert.ToInt32(baslangicbitis[1]);
            }
            else
            {
                sayac = Convert.ToInt32(id);
            }

            using (DBEntities entities = new DBEntities())
            {
                entities.ProjeEtkinliks.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            OrderByDescending(p => p.Tarih).
                                            Skip(sayac == 0 ? baslangic : 0).
                                            Take(sayac == 0 ? bitis : sayac).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileProjeEtkinlikData.Add(
                                                     new MobileProjeEtkinlikData
                                                     {
                                                         Baslik = p.Baslik,
                                                         Aciklama = p.Aciklama,
                                                         Tarih = p.Tarih,
                                                         DosyaYolu = p.DosyaYolu
                                                     })
                    );
            }

            return mobileProjeEtkinlikData.ToArray();
        }

        public static MobileCenazeData[] Cenaze()
        {
            List<MobileCenazeData> mobileCenazeData = new List<MobileCenazeData>();

            using (DBEntities entities = new DBEntities())
            {
                entities.Cenazes.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            OrderByDescending(p => p.Tarih).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileCenazeData.Add(
                                                     new MobileCenazeData
                                                     {
                                                         AdiSoyadi = p.AdiSoyadi,
                                                         BabaAdi = p.BabaAdi,
                                                         Tarih = p.Tarih,
                                                         DogumYeri = p.DogumYeri,
                                                         Iletisim = p.Iletisim,
                                                         Aciklama = p.Aciklama,
                                                     })
                    );
            }

            return mobileCenazeData.ToArray();
        }

        public static MobileRadyoTvData[] Radyo()
        {
            List<MobileRadyoTvData> mobileRadyoTvData = new List<MobileRadyoTvData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.RadyoTvs.
                                            AsNoTracking().
                                            Where(p => p.AktifMi &&
                                            p.RadyoTvTipKey == 1).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileRadyoTvData.Add(
                                                     new MobileRadyoTvData
                                                     {
                                                         Adi = p.Adi,
                                                         Link = p.Link,
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileRadyoTvData.ToArray();
        }

        public static MobileRadyoTvData[] Tv()
        {
            List<MobileRadyoTvData> mobileRadyoTvData = new List<MobileRadyoTvData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.RadyoTvs.
                                            AsNoTracking().
                                            Where(p => p.AktifMi &&
                                            p.RadyoTvTipKey == 2).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileRadyoTvData.Add(
                                                     new MobileRadyoTvData
                                                     {
                                                         Adi = p.Adi,
                                                         Link = p.Link,
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileRadyoTvData.ToArray();
        }

        public static MobileRadyoTvData[] RadyoTv(string id)
        {
            List<MobileRadyoTvData> mobileRadyoTvData = new List<MobileRadyoTvData>();
            int tipKey = Convert.ToInt32(id);
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.RadyoTvs.
                                            AsNoTracking().
                                            Where(p => p.AktifMi &&
                                            p.RadyoTvTipKey == tipKey).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileRadyoTvData.Add(
                                                     new MobileRadyoTvData
                                                     {
                                                         Adi = p.Adi,
                                                         Link = p.Link,
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileRadyoTvData.ToArray();
        }

        public static MobileDergiData[] Dergi()
        {
            List<MobileDergiData> mobileDergiData = new List<MobileDergiData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.Dergis.
                                            AsNoTracking().
                                            Where(p => p.AktifMi).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileDergiData.Add(
                                                     new MobileDergiData
                                                     {
                                                         DergiKey=p.DergiKey,
                                                         Adi = p.Adi,
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileDergiData.ToArray();
        }

        public static MobileDergiSayfaData[] DergiSayfa(string id)
        {
            List<MobileDergiSayfaData> mobileDergiSayfaData = new List<MobileDergiSayfaData>();
            using (DBEntities entities = new DBEntities())
            {
                var programAyar = entities.ProgramAyars.Single();
                entities.DergiSayfas.
                                            AsNoTracking().
                                            ToList().
                                            Where(p => p.AktifMi &&
                                            p.DergiKey == Convert.ToInt32(id)).
                                            ToList().
                                            OrderBy(p => p.SayfaNo).
                                            ToList().
                                            ForEach(
                                                     p =>
                                                     mobileDergiSayfaData.Add(
                                                     new MobileDergiSayfaData
                                                     {
                                                         SayfaNo = p.SayfaNo,
                                                         DosyaYolu = (programAyar.ServerAdres + @"/Uploads/Resim/" + p.DosyaYolu),
                                                     })
                    );
            }

            return mobileDergiSayfaData.ToArray();
        }

        public static void PushNotificationToAndroid(IEnumerable<string> registrationid, string duyuruId, string baslik)
        {
            var push = new PushBroker();
            push.RegisterGcmService(new GcmPushChannelSettings(ProgramIslemAyarBS.GoogleApiKeyGetir()));

            string jsonData = @"{""duyuruId"":""" + duyuruId + @""",""baslik"":""" + baslik + @"""}";
            push.QueueNotification(new GcmNotification().
                                   ForDeviceRegistrationId(registrationid)
                                  .WithJson(jsonData));
            push.StopAllServices();
        }

        public static void PushNotificationToIos(string registrationid, string duyuruId, string baslik)
        {
            var push = new PushBroker();
            var programAyar = new ProgramAyar();
            using (DBEntities entities = new DBEntities())
            {
                programAyar = entities.ProgramAyars.Single();
            }

            var appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"Uploads\Sertifika", programAyar.AppleSertifika));

            string jsonData = @"{""duyuruId"":""" + duyuruId + @""",""baslik"":""" + baslik + @"""}";
            push.RegisterAppleService(new ApplePushChannelSettings(false, appleCert, programAyar.AppleApiKey));
            push.QueueNotification(new AppleNotification().
                ForDeviceToken(registrationid).
                WithTag(jsonData).
                WithAlert(jsonData).
                WithSound("default").
                WithBadge(7));
        }

    }
}