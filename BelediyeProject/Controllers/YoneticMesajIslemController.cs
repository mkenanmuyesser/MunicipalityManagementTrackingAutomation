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
    public class YoneticiMesajIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            YoneticiMesajIslemViewModel yoneticiMesajIslemViewModel = new YoneticiMesajIslemViewModel();
            return View(yoneticiMesajIslemViewModel);
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
            YoneticiMesajIslemViewModel yoneticiMesajIslemViewModel = new YoneticiMesajIslemViewModel();
            yoneticiMesajIslemViewModel.Tarih = DateTime.Now;

            return View(yoneticiMesajIslemViewModel);
        }

        [HttpPost]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(YoneticiMesajIslemViewModel yoneticiMesajIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (YoneticiMesajIslemBS.YoneticiMesajKaydetGuncelle(yoneticiMesajIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "YoneticiMesajIslem");
            }
            else
            {
                return View(yoneticiMesajIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            YoneticiMesajIslemViewModel yoneticiMesajIslemViewModel = YoneticiMesajIslemBS.YoneticiMesajGetir(id);

            return View(yoneticiMesajIslemViewModel);
        }

        [HttpPost]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(YoneticiMesajIslemViewModel yoneticiMesajIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (YoneticiMesajIslemBS.YoneticiMesajKaydetGuncelle(yoneticiMesajIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "YoneticiMesajIslem");
            }
            else
            {
                return View(yoneticiMesajIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (YoneticiMesajIslemBS.YoneticiMesajSil(id))
            {
                return RedirectToAction("Index", "YoneticiMesajIslem");
            }
            else
            {
                return RedirectToAction("Index", "YoneticiMesajIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (YoneticiMesajIslemBS.YoneticiMesajResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "YoneticiMesajIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "YoneticiMesajIslem");
            }
        }

    }
}