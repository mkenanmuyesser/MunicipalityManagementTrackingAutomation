using BelediyeProject.Business;
using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BelediyeProject.Controllers
{
    public class KullaniciIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            KullaniciIslemViewModel kullaniciIslemViewModel = new KullaniciIslemViewModel();

            return View(kullaniciIslemViewModel);
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
            KullaniciIslemViewModel kullaniciIslemViewModel = new KullaniciIslemViewModel();
            kullaniciIslemViewModel.SecilenDosyaGonderilecekBirimTipList = new List<DosyaGonderilecekBirimTipData>();

            return View(kullaniciIslemViewModel);
        }

        [HttpPost]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(KullaniciIslemViewModel kullaniciIslemViewModel)
        {
            if (KullaniciIslemBS.KullaniciKaydetGuncelle(kullaniciIslemViewModel))
            {
                return RedirectToAction("Index", "KullaniciIslem");
            }
            else
            {
                return View(kullaniciIslemViewModel);
            }
        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(string id)
        {
            KullaniciIslemViewModel kullaniciIslemViewModel = null;

            Guid pKullaniciKey = Guid.Parse(id);
            kullaniciIslemViewModel = KullaniciIslemBS.KullaniciGetir(pKullaniciKey);
            kullaniciIslemViewModel.SecilenDosyaGonderilecekBirimTipList = KullaniciIslemBS.SecilenDosyaGonderilecekBirimTipGetir(Guid.Parse(id));

            return View(kullaniciIslemViewModel);
        }

        [HttpPost]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(KullaniciIslemViewModel kullaniciIslemViewModel)
        {
            if (KullaniciIslemBS.KullaniciKaydetGuncelle(kullaniciIslemViewModel))
            {
                return RedirectToAction("Index", "KullaniciIslem");
            }
            else
            {
                return View(kullaniciIslemViewModel);
            }
        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(string id)
        {
            Guid key = Guid.Parse(id);
            KullaniciIslemBS.KullaniciSil(key);
            return RedirectToAction("Index", "KullaniciIslem");
        }

        public ActionResult KullaniciAktif(string id)
        {
            Guid key = Guid.Parse(id);
            KullaniciIslemBS.KullaniciAktif(key);
            return RedirectToAction("Index", "KullaniciIslem");
        }
    }
}