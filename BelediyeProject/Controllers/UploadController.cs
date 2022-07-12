using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class UploadController : Controller
    {
        [HttpGet]
        public ActionResult Uploads()
        {
            var Mesaj = Request.QueryString["Mesaj"];
            var MailAdres = Request.QueryString["MailAdres"];
            var KullaniciIpAdres = Request.QueryString["KullaniciIpAdres"];
            var MesajTipNo = Convert.ToInt32(Request.QueryString["MesajTipNo"]);
            var GonderilecekBirimTipNo = Convert.ToInt32(Request.QueryString["GonderilecekBirimTipNo"]);
            var ResimUrls = Request.QueryString["ResimUrls"];
            var GpsKonum = Request.QueryString["GpsKonum"];

            //if (Request.QueryString.Count > 0)
            //{
            //    using (DBEntities entities = new DBEntities())
            //    {
            //        Dosya dosya = new Dosya
            //        {
            //            Mesaj=Mesaj,
            //            MailAdres = MailAdres,
            //            KullaniciIpAdres = KullaniciIpAdres,
            //            MesajTipNo = MesajTipNo,
            //            GonderilecekBirimTipNo = GonderilecekBirimTipNo,
            //            GpsKonum = GpsKonum,
            //            IslemZamani=DateTime.Now,
            //            DosyaDurumTipNo=1,
            //        };
            //        entities.Dosyas.Add(dosya);
             

            //        var resimler = ResimUrls.Split('|');
            //        foreach (var resim in resimler)
            //        {
            //            DosyaResim dosyaResim = new DosyaResim
            //            {
            //                Dosya= dosya,
            //                DosyaYolu= resim,
            //            };
            //            entities.DosyaResims.Add(dosyaResim);
            //        }

            //        entities.SaveChanges();
            //    }
            //}
            return View();
        }
    }
}