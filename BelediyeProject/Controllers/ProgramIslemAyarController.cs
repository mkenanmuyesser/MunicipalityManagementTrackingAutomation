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
    public class ProgramIslemAyarController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            ProgramIslemAyarViewModel programIslemAyarViewModel = ProgramIslemAyarBS.ProgramAyarGetir();
            return View(programIslemAyarViewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(ProgramIslemAyarViewModel programIslemAyarViewModel)
        {
            if (ProgramIslemAyarBS.ProgramAyarGuncelle(programIslemAyarViewModel))
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
            else
            {
                return View(programIslemAyarViewModel);
            }
        }

        [HttpPost]
        public ActionResult SertifikaYukle(HttpPostedFileBase file)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Sertifika");

            if (ProgramIslemAyarBS.ProgramAyarSertifikaEkle(file, dosyaYolu))
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
            else
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
        }

        [HttpGet]
        public ActionResult SertifikaSil()
        {
            if (ProgramIslemAyarBS.ProgramAyarSertifikaSil())
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
            else
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
        }

        [HttpPost]
        public ActionResult ResimYukle(HttpPostedFileBase file)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");

            if (ProgramIslemAyarBS.ProgramAyarResimEkle(file, dosyaYolu))
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
            else
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
        }

        [HttpGet]
        public ActionResult ResimSil()
        {
            if (ProgramIslemAyarBS.ProgramAyarResimSil())
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
            else
            {
                return RedirectToAction("Index", "ProgramIslemAyar");
            }
        }
    }
}