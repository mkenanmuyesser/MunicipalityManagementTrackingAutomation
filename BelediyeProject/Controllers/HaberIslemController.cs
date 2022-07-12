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
    public class HaberIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            HaberIslemViewModel haberIslemViewModel = new HaberIslemViewModel();
            return View(haberIslemViewModel);
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
            HaberIslemViewModel haberIslemViewModel = new HaberIslemViewModel();
            haberIslemViewModel.Tarih = DateTime.Now;

            return View(haberIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(HaberIslemViewModel haberIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (HaberIslemBS.HaberKaydetGuncelle(haberIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "HaberIslem");
            }
            else
            {
                return View(haberIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            HaberIslemViewModel haberIslemViewModel = HaberIslemBS.HaberGetir(id);

            return View(haberIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(HaberIslemViewModel haberIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (HaberIslemBS.HaberKaydetGuncelle(haberIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "HaberIslem");
            }
            else
            {
                return View(haberIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (HaberIslemBS.HaberSil(id))
            {
                return RedirectToAction("Index", "HaberIslem");
            }
            else
            {
                return RedirectToAction("Index", "HaberIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (HaberIslemBS.HaberResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "HaberIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "HaberIslem");
            }
        }

    }
}