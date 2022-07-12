using BelediyeProject.Business;
using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class TanimIslemBirimController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            TanimIslemBirimViewModel tanimIslemBirimViewModel = new TanimIslemBirimViewModel();

            return View(tanimIslemBirimViewModel);
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
            TanimIslemBirimViewModel tanimIslemBirimViewModel = new TanimIslemBirimViewModel();

            return View(tanimIslemBirimViewModel);
        }

        [HttpPost]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(TanimIslemBirimViewModel tanimIslemBirimViewModel)
        {
            if (TanimIslemBirimBS.BirimTipKaydetGuncelle(tanimIslemBirimViewModel))
            {
                return RedirectToAction("Index", "TanimIslemBirim");
            }
            else
            {
                return View(tanimIslemBirimViewModel);
            }
        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            TanimIslemBirimViewModel tanimIslemBirimViewModel = null;

            tanimIslemBirimViewModel = TanimIslemBirimBS.BirimTipGetir(id);

            return View(tanimIslemBirimViewModel);
        }

        [HttpPost]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(TanimIslemBirimViewModel tanimIslemBirimViewModel)
        {
            if (TanimIslemBirimBS.BirimTipKaydetGuncelle(tanimIslemBirimViewModel))
            {
                return RedirectToAction("Index", "TanimIslemBirim");
            }
            else
            {
                return View(tanimIslemBirimViewModel);
            }
        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            TanimIslemBirimBS.BirimTipSil(id);
            return RedirectToAction("Index", "TanimIslemBirim");
        }
    }
}