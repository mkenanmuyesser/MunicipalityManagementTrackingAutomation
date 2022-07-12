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
    public class ReklamIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            ReklamIslemViewModel reklamIslemViewModel = new ReklamIslemViewModel();
            return View(reklamIslemViewModel);
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
            ReklamIslemViewModel reklamIslemViewModel = new ReklamIslemViewModel();
            reklamIslemViewModel.BaslangicTarihi = DateTime.Now;
            reklamIslemViewModel.BitisTarihi = DateTime.Now.AddMonths(1);

            return View(reklamIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Kaydet")]
        public ActionResult KaydetPost(ReklamIslemViewModel reklamIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (ReklamIslemBS.ReklamKaydetGuncelle(reklamIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "ReklamIslem");
            }
            else
            {
                return View(reklamIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Guncelle")]
        public ActionResult GuncelleGet(int id)
        {
            ReklamIslemViewModel reklamIslemViewModel = ReklamIslemBS.ReklamGetir(id);

            return View(reklamIslemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ActionName("Guncelle")]
        public ActionResult GuncellePost(ReklamIslemViewModel reklamIslemViewModel)
        {
            string dosyaYolu = Server.MapPath("~/Uploads/Resim");
            if (ReklamIslemBS.ReklamKaydetGuncelle(reklamIslemViewModel, dosyaYolu))
            {
                return RedirectToAction("Index", "ReklamIslem");
            }
            else
            {
                return View(reklamIslemViewModel);
            }

        }

        [HttpGet]
        [ActionName("Sil")]
        public ActionResult SilGet(int id)
        {
            if (ReklamIslemBS.ReklamSil(id))
            {
                return RedirectToAction("Index", "ReklamIslem");
            }
            else
            {
                return RedirectToAction("Index", "ReklamIslem");
            }
        }

        [HttpGet]
        public ActionResult ResimSil(int id)
        {
            if (ReklamIslemBS.ReklamResimSil(id))
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "ReklamIslem");
            }
            else
            {
                return RedirectToAction("Guncelle/" + id.ToString(), "ReklamIslem");
            }
        }
    }
}