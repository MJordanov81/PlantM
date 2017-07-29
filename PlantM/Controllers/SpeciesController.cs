using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantM.Models;
using PlantM.Models.PlantModels;

namespace PlantM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpeciesController : Controller
    {
/*        SpeciesDbContext db = new SpeciesDbContext();*/

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
                using (var db = new ApplicationDbContext())
                {
                    db.Species.Add(species);
                    db.SaveChanges();
                    ViewBag.Message = "Hello"; 
                    return RedirectToAction("Create", "SpeciesLabel", new {confirmationMessage = $"Species {species.Name} has been created!"});
                }
            }

            return View();
        }
    }
}