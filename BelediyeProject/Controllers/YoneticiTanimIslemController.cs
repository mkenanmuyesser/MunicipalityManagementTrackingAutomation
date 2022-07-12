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
    public class YoneticiTanimIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            YoneticiTanimIslemViewModel yoneticiTanimIslemViewModel = YoneticiTanimIslemBS.YoneticiGetir();
            return View(yoneticiTanimIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Index")]
        public ActionResult IndexPost(YoneticiTanimIslemViewModel yoneticiTanimIslemViewModel)
        {
            if (YoneticiTanimIslemBS.YoneticiGuncelle(yoneticiTanimIslemViewModel))
            {
                return RedirectToAction("Index", "YoneticiTanimIslem");
            }
            else
            {
                return View(yoneticiTanimIslemViewModel);
            }
        }

        [HttpPost]
        public ActionResult ResimYukle(HttpPostedFileBase file)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");

            if (YoneticiTanimIslemBS.YoneticiResimEkle(file, dosyaYolu))
            {
                return RedirectToAction("Index", "YoneticiTanimIslem");
            }
            else
            {
                return RedirectToAction("Index", "YoneticiTanimIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (YoneticiTanimIslemBS.YoneticiResimsil(id))
            {
                return RedirectToAction("Index", "YoneticiTanimIslem");
            }
            else
            {
                return RedirectToAction("Index", "YoneticiTanimIslem");
            }
        }

    }
}