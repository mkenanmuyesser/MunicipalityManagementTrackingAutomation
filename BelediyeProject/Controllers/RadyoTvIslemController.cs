using BelediyeProject.Business;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class RadyoTvIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            RadyoTvIslemViewModel radyoTvIslemViewModel = new RadyoTvIslemViewModel();
            return View(radyoTvIslemViewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Kaydet")]
        public ActionResult KaydetGet()
        {
            RadyoTvIslemViewModel radyoTvIslemViewModel = new RadyoTvIslemViewModel();

            return View(radyoTvIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(RadyoTvIslemViewModel radyoTvIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (RadyoTvIslemBS.RadyoTvKaydetGuncelle(radyoTvIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "RadyoTvIslem");
            }
            else
            {
                return View(radyoTvIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            RadyoTvIslemViewModel radyoTvIslemViewModel = RadyoTvIslemBS.RadyoTvGetir(id);

            return View(radyoTvIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(RadyoTvIslemViewModel radyoTvIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (RadyoTvIslemBS.RadyoTvKaydetGuncelle(radyoTvIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "RadyoTvIslem");
            }
            else
            {
                return View(radyoTvIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (RadyoTvIslemBS.RadyoTvSil(id))
            {
                return RedirectToAction("Index", "RadyoTvIslem");
            }
            else
            {
                return RedirectToAction("Index", "RadyoTvIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (RadyoTvIslemBS.RadyoTvResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "RadyoTvIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "RadyoTvIslem");
            }
        }
    }
}