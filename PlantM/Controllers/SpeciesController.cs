using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantM.Models.PlantModels;

namespace PlantM.Controllers
{
    public class SpeciesController : Controller
    {
        SpeciesDbContext db = new SpeciesDbContext();

        // GET: Species
        public ActionResult Create()
        {
            return View();
        }

        // POST: Species/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Species species)
        {
            if (ModelState.IsValid)
            {
                db.Species.Add(species);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View();
        }
    }
}