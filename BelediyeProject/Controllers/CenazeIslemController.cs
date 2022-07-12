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
    public class CenazeIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            CenazeIslemViewModel cenazeIslemViewModel = new CenazeIslemViewModel();
            return View(cenazeIslemViewModel);
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
            CenazeIslemViewModel cenazeIslemViewModel = new CenazeIslemViewModel();
            cenazeIslemViewModel.Tarih = DateTime.Now;

            return View(cenazeIslemViewModel);
        }

        [HttpPost]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(CenazeIslemViewModel cenazeIslemViewModel)
        {
            if (CenazeIslemBS.CenazeKaydetGuncelle(cenazeIslemViewModel))
            {
                return RedirectToAction("Index", "CenazeIslem");
            }
            else
            {
                return View(cenazeIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            CenazeIslemViewModel cenazeIslemViewModel = CenazeIslemBS.CenazeGetir(id);

            return View(cenazeIslemViewModel);
        }

        [HttpPost]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(CenazeIslemViewModel cenazeIslemViewModel)
        {
            if (CenazeIslemBS.CenazeKaydetGuncelle(cenazeIslemViewModel))
            {
                return RedirectToAction("Index", "CenazeIslem");
            }
            else
            {
                return View(cenazeIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (CenazeIslemBS.CenazeSil(id))
            {
                return RedirectToAction("Index", "CenazeIslem");
            }
            else
            {
                return RedirectToAction("Index", "CenazeIslem");
            }
        }
    }
}