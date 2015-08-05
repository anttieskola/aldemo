using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aldemo.ui.Controllers
{
    public class GenerateController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "ALDemo - Generate";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? projects, int? lines)
        {
            if (projects == null || projects < 0 || projects > 100)
            {
                ModelState.AddModelError("projects", "project must be > 0 && < 100");
            }
            if (lines == null || lines < 0 || lines > 100)
            {
                ModelState.AddModelError("lines", "lines must be > 0 && < 100");
            }
            if (ModelState.IsValid)
            {
                // run generate
                launchGenerate();
                return View("Run");
            }
            return View();
        }

        private void launchGenerate()
        {
            
        }
    }
}

