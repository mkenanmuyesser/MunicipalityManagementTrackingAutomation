using BelediyeProject.Business;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BelediyeProject.Controllers
{
    public class ProgramIslemLogController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult IndexGet()
        {
            ProgramIslemLogViewModel programIslemLogViewModel = ProgramIslemLogBS.ProgramLogGetir();
            return View(programIslemLogViewModel);
        }
                
    }
}