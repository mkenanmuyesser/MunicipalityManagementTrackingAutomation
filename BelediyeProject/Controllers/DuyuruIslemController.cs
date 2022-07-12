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
    public class DuyuruIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            DuyuruIslemViewModel duyuruIslemViewModel = new DuyuruIslemViewModel();
            return View(duyuruIslemViewModel);
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
            DuyuruIslemViewModel duyuruIslemViewModel = new DuyuruIslemViewModel();
            return View(duyuruIslemViewModel);
        }

        [HttpPost]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(DuyuruIslemViewModel duyuruIslemViewModel)
        {
            if (DuyuruIslemBS.DuyuruKaydetGuncelle(duyuruIslemViewModel))
            {
                return RedirectToAction("Index", "DuyuruIslem");
            }
            else
            {
                return View(duyuruIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            DuyuruIslemViewModel duyuruIslemViewModel = DuyuruIslemBS.DuyuruGetir(id);

            return View(duyuruIslemViewModel);
        }

        [HttpPost]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(DuyuruIslemViewModel duyuruIslemViewModel)
        {
            if (DuyuruIslemBS.DuyuruKaydetGuncelle(duyuruIslemViewModel))
            {
                return RedirectToAction("Index", "DuyuruIslem");
            }
            else
            {
                return View(duyuruIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (DuyuruIslemBS.DuyuruSil(id))
            {
                return RedirectToAction("Index", "DuyuruIslem");
            }
            else
            {
                return RedirectToAction("Index", "DuyuruIslem");
            }
        }
    }
}