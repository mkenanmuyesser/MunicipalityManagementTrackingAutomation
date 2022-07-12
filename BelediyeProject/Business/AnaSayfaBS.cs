using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BelediyeProject.Business
{
    public class AnaSayfaBS
    {
        public static List<MenuData> MenuOlustur()
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();
            List<MenuData> menu = new List<MenuData>();
            AnaMenuOlustur(ref menu);

            switch (kullaniciData.KullaniciRolTipKey)
            {
                case 1:
                case 2:
                    ButunMenuOlustur(ref menu);
                    break;
                case 4:
                    OperatorMenuOlustur(ref menu);
                    break;
                case 5:
                    BirimSorumlusuMenuOlustur(ref menu);
                    break;
            }
            return menu;
        }

        private static void AnaMenuOlustur(ref List<MenuData> menu)
        {
            MenuData menuAnaSayfa = new MenuData
            {
                Id = 0,
                Adi = "AnaSayfa",
                Link = "/AnaSayfa/Index",
                AktifMi = false,
                UstMenuId = null,
            };
            menu.Add(menuAnaSayfa);
        }

        private static void ButunMenuOlustur(ref List<MenuData> menu)
        {

            #region KullaniciIslem

            MenuData menuKullaniciIslem = new MenuData
            {
                Id = 1,
                Adi = "Kullanıcı İşlemleri",
                Link = "/KullaniciIslem/Index",
                AktifMi = false,
                UstMenuId = null,
            };
            menu.Add(menuKullaniciIslem);

            #endregion

            #region ProgramIslem

            MenuData menuProgramIslem = new MenuData
            {
                Id = 2,
                Adi = "Program İşlemleri",
                Link = "",
                AktifMi = false,
                UstMenuId = null,
            };
            MenuData menuProgramIslemAyar = new MenuData
            {
                Id = 3,
                Adi = "Ayarlar",
                Link = "/ProgramIslemAyar/Index",
                AktifMi = false,
                UstMenuId = 2,
            };
            MenuData menuProgramIslemLog = new MenuData
            {
                Id = 4,
                Adi = "Program Logları",
                Link = "/ProgramIslemLog/Index",
                AktifMi = false,
                UstMenuId = 2,
            };
            menu.Add(menuProgramIslem);
            menu.Add(menuProgramIslemAyar);
            menu.Add(menuProgramIslemLog);

            #endregion

            #region YoneticiIslem

            MenuData menuYoneticiIslem = new MenuData
            {
                Id = 5,
                Adi = "Yönetici İşlemleri",
                Link = "",
                AktifMi = false,
                UstMenuId = null,
            };
            MenuData menuYoneticiTanimIslem = new MenuData
            {
                Id = 6,
                Adi = "Yönetici Bilgileri",
                Link = "/YoneticiTanimIslem/Index",
                AktifMi = true,
                UstMenuId = 5,
            };
            MenuData menuYoneticiMesajIslem = new MenuData
            {
                Id = 7,
                Adi = "Yönetici Mesajları",
                Link = "/YoneticiMesajIslem/Index",
                AktifMi = true,
                UstMenuId = 5,
            };
         
            menu.Add(menuYoneticiIslem);
            menu.Add(menuYoneticiTanimIslem);
            menu.Add(menuYoneticiMesajIslem);

            #endregion

            #region TanitimIslem

            MenuData menuDuyuruIslem = new MenuData
            {
                Id = 8,
                Adi = "Duyuru İşlemleri",
                Link = "/DuyuruIslem/Index",
                AktifMi = true,
                UstMenuId = null,
            };

            menu.Add(menuDuyuruIslem);

            #endregion

            #region HaberIslem

            MenuData menuHaberIslem = new MenuData
            {
                Id = 9,
                Adi = "Haber İşlemleri",
                Link = "/HaberIslem/Index",
                AktifMi = true,
                UstMenuId = null,
            };

            menu.Add(menuHaberIslem);

            #endregion

            #region ProjeEtkinlikIslem

            MenuData menuProjeEtkinlikIslem = new MenuData
            {
                Id = 10,
                Adi = "Proje ve Etkinlik İşlemleri",
                Link = "/ProjeEtkinlikIslem/Index",
                AktifMi = true,
                UstMenuId = null,
            };

            menu.Add(menuProjeEtkinlikIslem);

            #endregion

            #region CenazeIslem

            MenuData menuCenazeIslem = new MenuData
            {
                Id = 11,
                Adi = "Cenaze İşlemleri",
                Link = "/CenazeIslem/Index",
                AktifMi = true,
                UstMenuId = null,
            };

            menu.Add(menuCenazeIslem);

            #endregion         

            OperatorMenuOlustur(ref menu);

            #region ReklamIslem

            MenuData menuReklamIslem = new MenuData
            {
                Id = 1000,
                Adi = "Reklam İşlemleri",
                Link = "/ReklamIslem/Index",
                AktifMi = false,
                UstMenuId = null,
            };
            menu.Add(menuReklamIslem);

            #endregion

            #region LogoBannerIslem

            MenuData menuLogoBannerIslem = new MenuData
            {
                Id = 1001,
                Adi = "Logo-Banner-Galeri İşlemleri",
                Link = "",
                AktifMi = false,
                UstMenuId = null,
            };

            MenuData menuBannerIslem = new MenuData
            {
                Id = 1002,
                Adi = "Banner İşlemleri",
                Link = "/BannerIslem/Index",
                AktifMi = true,
                UstMenuId = 1001,
            };

            MenuData menuLogoIslem = new MenuData
            {
                Id = 1003,
                Adi = "Logo İşlemleri",
                Link = "/LogoIslem/Index",
                AktifMi = true,
                UstMenuId = 1001,
            };

            MenuData menuGaleriIslem = new MenuData
            {
                Id = 1004,
                Adi = "Galeri İşlemleri",
                Link = "/GaleriIslem/Index",
                AktifMi = true,
                UstMenuId = 1001,
            };

            menu.Add(menuLogoBannerIslem);
            menu.Add(menuBannerIslem);
            menu.Add(menuLogoIslem);
            menu.Add(menuGaleriIslem);

            #endregion

            #region RadyoTvIslem

            MenuData menuRadyoTvIslem = new MenuData
            {
                Id = 1005,
                Adi = "Radyo-Tv İşlemleri",
                Link = "/RadyoTvIslem/Index",
                AktifMi = false,
                UstMenuId = null,
            };

            menu.Add(menuRadyoTvIslem);

            #endregion

            #region DergiTanimIslem

            MenuData menuDergiTanimIslem = new MenuData
            {
                Id = 1006,
                Adi = "Dergi İşlemleri",
                Link = "",
                AktifMi = false,
                UstMenuId = null,
            };
            MenuData menuDergiTanimIslemDergi = new MenuData
            {
                Id = 1007,
                Adi = "Dergi Tanımlama İşlemleri",
                Link = "/DergiIslem/Index",
                AktifMi = false,
                UstMenuId = 1005,
            };
            MenuData menuDergiTanimIslemDergiSayfa = new MenuData
            {
                Id = 1008,
                Adi = "Dergi Sayfa Tanımlama İşlemleri",
                Link = "/DergiSayfaIslem/Index",
                AktifMi = false,
                UstMenuId = 1005,
            };

            menu.Add(menuDergiTanimIslem);
            menu.Add(menuDergiTanimIslemDergi);
            menu.Add(menuDergiTanimIslemDergiSayfa);

            #endregion

            #region TanimIslem

            MenuData menuTanimIslem = new MenuData
            {
                Id = 1009,
                Adi = "Tanım İşlemleri",
                Link = "",
                AktifMi = false,
                UstMenuId = null,
            };
            MenuData menuTanimIslemBirim = new MenuData
            {
                Id = 1010,
                Adi = "Birim Tanımlama İşlemleri",
                Link = "/TanimIslemBirim/Index",
                AktifMi = false,
                UstMenuId = 1009,
            };
           
            menu.Add(menuTanimIslem);
            menu.Add(menuTanimIslemBirim);

            #endregion

            #region RaporIslem

            MenuData menuRaporIslem = new MenuData
            {
                Id = 9999,
                Adi = "Raporlar",
                Link = "/RaporIslem/Index",
                AktifMi = false,
                UstMenuId = null,
            };
            menu.Add(menuRaporIslem);

            #endregion
        }

        private static void OperatorMenuOlustur(ref List<MenuData> menu)
        {
            #region DosyaIslem

            MenuData menuDosyaIslem = new MenuData
            {
                Id = 8,
                Adi = "Dosya İşlemleri",
                Link = "",
                AktifMi = false,
                UstMenuId = null,
            };
            menu.Add(menuDosyaIslem);

            using (DBEntities entities = new DBEntities())
            {
                int idSira = 9;
                foreach (var dosyaGonderilecekBirimTip in entities.tt_DosyaGonderilecekBirimTip.
                                                                                                AsNoTracking().
                                                                                                Where(p => p.AktifMi).
                                                                                                ToList())
                {
                    MenuData menuDosyaIslemGonderilecekBirimTip = new MenuData
                    {
                        Id = idSira,
                        Adi = dosyaGonderilecekBirimTip.DosyaGonderilecekBirimTipAdi,
                        Link = string.Format("/DosyaIslem/Index/{0}", dosyaGonderilecekBirimTip.DosyaGonderilecekBirimTipKey),
                        AktifMi = false,
                        UstMenuId = 8,
                    };
                    menu.Add(menuDosyaIslemGonderilecekBirimTip);
                    idSira++;
                }
            }

            #endregion
        }

        private static void BirimSorumlusuMenuOlustur(ref List<MenuData> menu)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            #region DosyaIslem

            using (DBEntities entities = new DBEntities())
            {
                int idSira = 1;

                var kullanici = entities.Kullanicis.
                                                   AsNoTracking().
                                                   Include("KullaniciBirims").
                                                   Include("KullaniciBirims.tt_DosyaGonderilecekBirimTip").
                                                   SingleOrDefault(p => p.KullaniciKey == kullaniciData.KullaniciKey);


                foreach (var kullaniciBirim in kullanici.KullaniciBirims)
                {
                    if (kullaniciBirim.AktifMi)
                    {
                        MenuData menuDosyaIslemGonderilecekBirimTip = new MenuData
                        {
                            Id = idSira,
                            Adi = kullaniciBirim.tt_DosyaGonderilecekBirimTip.DosyaGonderilecekBirimTipAdi,
                            Link = string.Format("/DosyaIslem/Index/{0}", kullaniciBirim.tt_DosyaGonderilecekBirimTip.DosyaGonderilecekBirimTipKey),
                            AktifMi = false,
                            UstMenuId = null,
                        };
                        menu.Add(menuDosyaIslemGonderilecekBirimTip);
                        idSira++;
                    }
                }
            }

            #endregion
        }
    }
}