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
    public class ProjeEtkinlikIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            ProjeEtkinlikIslemViewModel projeEtkinlikIslemViewModel = new ProjeEtkinlikIslemViewModel();
            return View(projeEtkinlikIslemViewModel);
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
            ProjeEtkinlikIslemViewModel projeEtkinlikIslemViewModel = new ProjeEtkinlikIslemViewModel();
            projeEtkinlikIslemViewModel.Tarih = DateTime.Now;

            return View(projeEtkinlikIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(ProjeEtkinlikIslemViewModel projeEtkinlikIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (ProjeEtkinlikIslemBS.ProjeEtkinlikKaydetGuncelle(projeEtkinlikIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "ProjeEtkinlikIslem");
            }
            else
            {
                return View(projeEtkinlikIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            ProjeEtkinlikIslemViewModel projeEtkinlikIslemViewModel = ProjeEtkinlikIslemBS.ProjeEtkinlikGetir(id);

            return View(projeEtkinlikIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(ProjeEtkinlikIslemViewModel projeEtkinlikIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (ProjeEtkinlikIslemBS.ProjeEtkinlikKaydetGuncelle(projeEtkinlikIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "ProjeEtkinlikIslem");
            }
            else
            {
                return View(projeEtkinlikIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (ProjeEtkinlikIslemBS.ProjeEtkinlikSil(id))
            {
                return RedirectToAction("Index", "ProjeEtkinlikIslem");
            }
            else
            {
                return RedirectToAction("Index", "ProjeEtkinlikIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (ProjeEtkinlikIslemBS.ProjeEtkinlikResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "ProjeEtkinlikIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "ProjeEtkinlikIslem");
            }
        }

    }
}