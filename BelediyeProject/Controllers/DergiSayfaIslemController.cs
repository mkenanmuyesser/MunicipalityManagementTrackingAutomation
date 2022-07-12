using BelediyeProject.Business;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class DergiSayfaIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            DergiSayfaIslemViewModel dergiSayfaIslemViewModel = new DergiSayfaIslemViewModel();
            return View(dergiSayfaIslemViewModel);
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
            DergiSayfaIslemViewModel dergiSayfaIslemViewModel = new DergiSayfaIslemViewModel();

            return View(dergiSayfaIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(DergiSayfaIslemViewModel dergiSayfaIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (DergiSayfaIslemBS.DergiSayfaKaydetGuncelle(dergiSayfaIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "DergiSayfaIslem");
            }
            else
            {
                return View(dergiSayfaIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            DergiSayfaIslemViewModel dergiSayfaIslemViewModel = DergiSayfaIslemBS.DergiSayfaGetir(id);

            return View(dergiSayfaIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(DergiSayfaIslemViewModel dergiSayfaIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (DergiSayfaIslemBS.DergiSayfaKaydetGuncelle(dergiSayfaIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "DergiSayfaIslem");
            }
            else
            {
                return View(dergiSayfaIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (DergiSayfaIslemBS.DergiSayfaSil(id))
            {
                return RedirectToAction("Index", "DergiSayfaIslem");
            }
            else
            {
                return RedirectToAction("Index", "DergiSayfaIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (DergiSayfaIslemBS.DergiSayfaResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "DergiSayfaIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "DergiSayfaIslem");
            }
        }
    }
}