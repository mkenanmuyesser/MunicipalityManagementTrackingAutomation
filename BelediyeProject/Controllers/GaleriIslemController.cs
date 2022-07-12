using BelediyeProject.Business;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class GaleriIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            GaleriIslemViewModel galeriIslemViewModel = new GaleriIslemViewModel();
            return View(galeriIslemViewModel);
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
            GaleriIslemViewModel galeriIslemViewModel = new GaleriIslemViewModel();

            return View(galeriIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(GaleriIslemViewModel galeriIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (GaleriIslemBS.GaleriKaydetGuncelle(galeriIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "GaleriIslem");
            }
            else
            {
                return View(galeriIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            GaleriIslemViewModel galeriIslemViewModel = GaleriIslemBS.GaleriGetir(id);

            return View(galeriIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(GaleriIslemViewModel galeriIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (GaleriIslemBS.GaleriKaydetGuncelle(galeriIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "GaleriIslem");
            }
            else
            {
                return View(galeriIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (GaleriIslemBS.GaleriSil(id))
            {
                return RedirectToAction("Index", "GaleriIslem");
            }
            else
            {
                return RedirectToAction("Index", "GaleriIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (GaleriIslemBS.GaleriResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "GaleriIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "GaleriIslem");
            }
        }
    }
}