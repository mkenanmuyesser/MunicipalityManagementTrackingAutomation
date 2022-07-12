using BelediyeProject.Business;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class BannerIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            BannerIslemViewModel bannerIslemViewModel = new BannerIslemViewModel();
            return View(bannerIslemViewModel);
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
            BannerIslemViewModel bannerIslemViewModel = new BannerIslemViewModel();

            return View(bannerIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(BannerIslemViewModel bannerIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (BannerIslemBS.BannerKaydetGuncelle(bannerIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "BannerIslem");
            }
            else
            {
                return View(bannerIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            BannerIslemViewModel bannerIslemViewModel = BannerIslemBS.BannerGetir(id);

            return View(bannerIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(BannerIslemViewModel bannerIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (BannerIslemBS.BannerKaydetGuncelle(bannerIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "BannerIslem");
            }
            else
            {
                return View(bannerIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (BannerIslemBS.BannerSil(id))
            {
                return RedirectToAction("Index", "BannerIslem");
            }
            else
            {
                return RedirectToAction("Index", "BannerIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (BannerIslemBS.BannerResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "BannerIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "BannerIslem");
            }
        }
    }
}