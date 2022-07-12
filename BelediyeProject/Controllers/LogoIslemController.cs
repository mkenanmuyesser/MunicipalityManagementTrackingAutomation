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
    public class LogoIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            LogoIslemViewModel logoIslemViewModel = LogoIslemBS.LogoGetir();
            return View(logoIslemViewModel);
        }

        [HttpPost]
        public ActionResult ResimYukle(HttpPostedFileBase file)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");

            if (LogoIslemBS.LogoResimEkle(file, dosyaYolu))
            {
                return RedirectToAction("Index", "LogoIslem");
            }
            else
            {
                return RedirectToAction("Index", "LogoIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (LogoIslemBS.LogoResimsil(id))
            {
                return RedirectToAction("Index", "LogoIslem");
            }
            else
            {
                return RedirectToAction("Index", "LogoIslem");
            }
        }

    }
}