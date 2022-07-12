using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class AnaSayfaController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            AnaSayfaViewModel anaSayfaViewModel = new AnaSayfaViewModel();
            return View(anaSayfaViewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Cikis")]
        public ActionResult CikisGet()
        {
            Session["KullaniciData"] = null;
            return RedirectToAction("Index", "GirisIslem");
        }
    }
}