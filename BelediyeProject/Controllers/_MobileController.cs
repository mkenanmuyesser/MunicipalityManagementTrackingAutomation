using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BelediyeProject.Controllers
{    

    public class KullaniciController : ApiController
    {
        public bool Post(MobileKullaniciData mobileKullaniciData)
        {
            if (ModelState.IsValid && mobileKullaniciData != null)
            {
                var sonuc = MobileBS.KullaniciKayit(mobileKullaniciData);
                return sonuc;
            }
            else
            {
                return false;
            }
        }
    }

    public class DeviceTokenController : ApiController
    {
        public bool Post(MobileKullaniciData mobileKullaniciData)
        {
            if (ModelState.IsValid && mobileKullaniciData != null)
            {
                var sonuc = MobileBS.KullaniciKayit(mobileKullaniciData);
                return sonuc;
            }
            else
            {
                return false;
            }
        }
    }

    public class DosyaController : ApiController
    {
        public MobileDosyaDurumData[] Get(string id)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(id))
            {
                var sonuc = MobileBS.DosyaDurum(id);
                return sonuc;
            }
            else
            {
                return null;
            }
        }

        public bool Post(MobileDosyaData mobileDosyaData)
        {
            if (ModelState.IsValid && mobileDosyaData != null)
            {
                var sonuc = MobileBS.DosyaKayit(mobileDosyaData);
                return sonuc;
            }
            else
            {
                return false;
            }
        }
    }

    public class DosyaHashController : ApiController
    {
        public bool Post(MobileDosyaHashData mobileDosyaHashData)
        {
            if (ModelState.IsValid && mobileDosyaHashData != null)
            {
                var sonuc = MobileBS.DosyaHashKayit(mobileDosyaHashData);
                return sonuc;
            }
            else
            {
                return false;
            }
        }
    }

    public class DosyaResimController : ApiController
    {
        public HttpResponseMessage Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var dosyaAdi = postedFile.FileName.Trim('\"');
                    string dosyaYolu = HttpContext.Current.Server.MapPath("~/Uploads/Resim");
                    var path = Path.Combine(dosyaYolu, dosyaAdi);
                    postedFile.SaveAs(path);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }

    public class DosyaVideoController : ApiController
    {
        public HttpResponseMessage Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var dosyaAdi = postedFile.FileName.Trim('\"');
                    string dosyaYolu = HttpContext.Current.Server.MapPath("~/Uploads/Video");
                    var path = Path.Combine(dosyaYolu, dosyaAdi);
                    postedFile.SaveAs(path);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }

    //-------------------------------------------

    public class ReklamController : ApiController
    {
        public MobileReklamData[] Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Reklam();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class YoneticiController : ApiController
    {
        public MobileYoneticiData Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Yonetici();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class YoneticiMesajController : ApiController
    {
        public MobileYoneticiMesajData[] Get(string id)
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.YoneticiMesaj(id);
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class YoneticiResimController : ApiController
    {
        public MobileLogoResimData Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.YoneticiResim();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class BannerController : ApiController
    {
        public MobileBannerData[] Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.BannerResim();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class GaleriController : ApiController
    {
        public MobileGaleriData[] Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.GaleriResim();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class LogoController : ApiController
    {
        public MobileLogoResimData Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.LogoResim();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class SayfaAdresController : ApiController
    {
        public MobileSayfaAdresData Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.SayfaAdres();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class RadyoController : ApiController
    {
        public MobileRadyoTvData[] Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Radyo();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class TvController : ApiController
    {
        public MobileRadyoTvData[] Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Tv();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class RadyoTvController : ApiController
    {
        public MobileRadyoTvData[] Get(string id)
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.RadyoTv(id);
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class DergiController : ApiController
    {
        public MobileDergiData[] Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Dergi();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class DergiSayfaController : ApiController
    {
        public MobileDergiSayfaData[] Get(string id)
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.DergiSayfa(id);
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class DuyuruController : ApiController
    {
        public MobileDuyuruData Get(string id)
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Duyuru(id);
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class DuyuruMesajController : ApiController
    {
        public MobileDuyuruData[] Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Duyuru();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class HaberController : ApiController
    {
        public MobileHaberData[] Get(string id)
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Haber(id);
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class ProjeEtkinlikController : ApiController
    {
        public MobileProjeEtkinlikData[] Get(string id)
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.ProjeEtkinlik(id);
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class CenazeController : ApiController
    {
        public MobileCenazeData[] Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Cenaze();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

    public class LimitController : ApiController
    {
        public MobileLimitData Get()
        {
            if (ModelState.IsValid)
            {
                var sonuc = MobileBS.Limit();
                return sonuc;
            }
            else
            {
                return null;
            }
        }
    }

}
