using BelediyeProject.Entities;
using BelediyeProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BelediyeProject.Business
{
    public class GirisIslemBS
    {
        public static bool GirisDogrula(string kullaniciAdi, string sifre)
        {
            sifre = CryptoHelper.Sifrele(sifre);

            using (DBEntities entities = new DBEntities())
            {
                if (entities.Kullanicis.
                                        AsNoTracking().
                                        ToList().
                                        Any(p => p.AktifMi &&
                                                 GirisKullaniciRolDogrulama(p.KullaniciRolTipKey) &&
                                                 p.KullaniciAdi.ToLower() == kullaniciAdi &&
                                                 p.Sifre == sifre))
                {                   
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void GirisYap(string kullaniciAdi)
        {
            FormsAuthentication.SetAuthCookie(kullaniciAdi, true);          
        }

        public static KullaniciData KullaniciDataGetir()
        {
            return HttpContext.Current.Session["KullaniciData"] as KullaniciData;
        }

        public static KullaniciData GirisYapanKullaniciDataGetir(string girisYapanKullaniciAdi)
        {
            KullaniciData kullaniciData = new KullaniciData();

            using (DBEntities entities = new DBEntities())
            {
                var kullanici = entities.Kullanicis.
                                                  AsNoTracking().
                                                  SingleOrDefault(p => p.KullaniciAdi.ToLower() == girisYapanKullaniciAdi);
                if (kullanici == null)
                {
                    return kullaniciData;
                }
                else
                {
                    kullaniciData.KullaniciKey = kullanici.KullaniciKey;
                    kullaniciData.KullaniciAdi = kullanici.KullaniciAdi;
                    kullaniciData.KullaniciRolTipKey = kullanici.KullaniciRolTipKey;

                    return kullaniciData;
                }
            }

        }

        private static bool GirisKullaniciRolDogrulama(int kullaniciRolTipKey)
        {
            if (kullaniciRolTipKey == 1 ||
                kullaniciRolTipKey == 2 ||
                kullaniciRolTipKey == 4 ||
                kullaniciRolTipKey == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
          
    }
}