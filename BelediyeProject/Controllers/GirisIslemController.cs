using BelediyeProject.Business;
using BelediyeProject.Entities;
using BelediyeProject.Helpers;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BelediyeProject.Controllers
{
    public class GirisIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            GirisIslemViewModel girisViewModel = new GirisIslemViewModel();
            return View(girisViewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(GirisIslemViewModel girisViewModel)
        {
            if (ModelState.IsValid)
            {
                string kullaniciAdi = girisViewModel.KullaniciAdi.ToLower();
                string sifre = girisViewModel.Sifre;

                if (GirisIslemBS.GirisDogrula(kullaniciAdi, sifre))
                {
                    Session["KullaniciData"] = GirisIslemBS.GirisYapanKullaniciDataGetir(kullaniciAdi); 
                    return RedirectToAction("Index", "AnaSayfa");                   
                }
                else
                {
                    girisViewModel.Sonuc = "Giriş işlemi başarısızdır.";
                    return View(girisViewModel);
                }
            }
            return View();
        }
    }
}