using BelediyeProject.Business;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class DergiIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            DergiIslemViewModel dergiIslemViewModel = new DergiIslemViewModel();
            return View(dergiIslemViewModel);
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
            DergiIslemViewModel dergiIslemViewModel = new DergiIslemViewModel();

            return View(dergiIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(DergiIslemViewModel dergiIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (DergiIslemBS.DergiKaydetGuncelle(dergiIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "DergiIslem");
            }
            else
            {
                return View(dergiIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            DergiIslemViewModel dergiIslemViewModel = DergiIslemBS.DergiGetir(id);

            return View(dergiIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(DergiIslemViewModel dergiIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (DergiIslemBS.DergiKaydetGuncelle(dergiIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "DergiIslem");
            }
            else
            {
                return View(dergiIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (DergiIslemBS.DergiSil(id))
            {
                return RedirectToAction("Index", "DergiIslem");
            }
            else
            {
                return RedirectToAction("Index", "DergiIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (DergiIslemBS.DergiResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "DergiIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "DergiIslem");
            }
        }
    }
}