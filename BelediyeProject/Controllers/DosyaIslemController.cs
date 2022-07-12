
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
    public class DosyaIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet(int id)
        {
            DosyaIslemViewModel dosyaIslemViewModel = new DosyaIslemViewModel
            {
                DosyaGonderilecekBirimTipKey = id,
                BaslangicGelen = DateTime.Now.Date.AddDays(-1),
                BitisGelen = DateTime.Now.Date,
                BaslangicIslemYapilmis = DateTime.Now.Date.AddDays(-1),
                BitisIslemYapilmis = DateTime.Now.Date,
            };
            dosyaIslemViewModel = DosyaIslemBS.DosyaDetaylariGetir(ref dosyaIslemViewModel);

            return View(dosyaIslemViewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(DosyaIslemViewModel dosyaIslemViewModel)
        {
            dosyaIslemViewModel = DosyaIslemBS.DosyaDetaylariGetir(ref dosyaIslemViewModel);
            return View(dosyaIslemViewModel);
        }

        [HttpGet]
        [ActionName("DetayOlustur")]
        public ActionResult DetayOlusturGet()
        {
            DosyaIslemViewModel dosyaIslemViewModel = new DosyaIslemViewModel();
            return View(dosyaIslemViewModel);
        }

        [HttpGet]
        [ActionName("Detaylar")]
        public ActionResult DetaylarGet(int id)
        {
            DosyaIslemViewModel dosyaIslemViewModel = DosyaIslemBS.DosyaGetir(id);
            return View(dosyaIslemViewModel);
        }

        [HttpPost]
        [ActionName("Detaylar")]
        public ActionResult DetaylarPost(Dosya dosya)
        {
            return View(dosya);
        }

        [HttpGet]
        [ActionName("Yazdir")]
        public ActionResult YazdirGet(int id)
        {
            DosyaIslemViewModel dosyaIslemViewModel = DosyaIslemBS.DosyaGetir(id);
            return View(dosyaIslemViewModel);
        }

        public ActionResult BildirimGonder(DosyaIslemViewModel dosyaIslemViewModel)
        {
            DosyaIslemBS.BildirimGonder(dosyaIslemViewModel);
            return RedirectToAction("Detaylar", "DosyaIslem", new { id = dosyaIslemViewModel.DosyaKey });
        }

        public ActionResult KaraListeyeAl(DosyaIslemViewModel dosyaIslemViewModel)
        {
            DosyaIslemBS.KaraListeyeAl(dosyaIslemViewModel);
            return RedirectToAction("Detaylar", "DosyaIslem", new { id = dosyaIslemViewModel.DosyaKey });
        }

        public ActionResult Yonlendir(DosyaIslemViewModel dosyaIslemViewModel)
        {
            DosyaIslemBS.DosyaYonlendir(dosyaIslemViewModel);
            return RedirectToAction("Index", "DosyaIslem", new { id = dosyaIslemViewModel.DosyaGonderilecekBirimTipKey });
        }

        public ActionResult KabulEt(DosyaIslemViewModel dosyaIslemViewModel)
        {
            int? dosyaGonderilecekBirimTipKey = DosyaIslemBS.DosyaKabulEt(dosyaIslemViewModel);
            return RedirectToAction("Index", "DosyaIslem", new { id = dosyaGonderilecekBirimTipKey.Value });
        }

        public ActionResult Reddet(DosyaIslemViewModel dosyaIslemViewModel)
        {
           int? dosyaGonderilecekBirimTipKey =  DosyaIslemBS.DosyaReddet(dosyaIslemViewModel);
            return RedirectToAction("Index", "DosyaIslem", new { id = dosyaGonderilecekBirimTipKey.Value });
        }

        public ActionResult Sil(DosyaIslemViewModel dosyaIslemViewModel)
        {
            int? dosyaGonderilecekBirimTipKey = DosyaIslemBS.DosyaSil(dosyaIslemViewModel);
            return RedirectToAction("Index", "DosyaIslem", new { id = dosyaGonderilecekBirimTipKey.Value });
        }
    }
}