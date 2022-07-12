using BelediyeProject.Business;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class RaporIslemController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            RaporIslemViewModel raporIslemViewModel = new RaporIslemViewModel
            {
                #region Islem Raporları

                IslemRaporBaslangic = DateTime.Now.Date.AddDays(-1),
                IslemRaporBitis = DateTime.Now.Date,

                IslemRaporAktifMi=true,

                #endregion

                #region Birime Gore Kullanım Raporları

                BirimeGoreKullanimRaporBaslangic= DateTime.Now.Date.AddDays(-1),
                BirimeGoreKullanimRaporBitis = DateTime.Now.Date,

                #endregion

            };
            raporIslemViewModel = RaporIslemBS.RaporGetir(ref raporIslemViewModel);

            return View(raporIslemViewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(RaporIslemViewModel raporIslemViewModel)
        {
            raporIslemViewModel = RaporIslemBS.RaporGetir(ref raporIslemViewModel);
            return View(raporIslemViewModel);
        }
    }
}