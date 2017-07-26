using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using PlantM.Models.PlantModels;

namespace PlantM.Controllers
{
    public class SpeciesLabelController : Controller
    {
        SpeciesLabelDbContext db = new SpeciesLabelDbContext();
        CustomGroupDbContext customGroupsDb = new CustomGroupDbContext();
        FamilyDbContext familyDb = new FamilyDbContext();
        GenusDbContext genusDb = new GenusDbContext();
        SpeciesDbContext speciesDb = new SpeciesDbContext();

        // GET: SpeciesLabel
        public ActionResult Create()
        {
            List<SelectListItem> customGroups = new List<SelectListItem>();
            IQueryable<string> customGroupsQuery = from g in customGroupsDb.CustomGroup
                select g.Name;

            foreach (var element in customGroupsQuery)
            {
                customGroups.Add(new SelectListItem()
                {
                    Value = element,
                    Text = element
                });
            }

            ViewBag.CustomGroupList = customGroups;

            IQueryable<string> familyQuery = from g in familyDb.Family
                select g.Name;
            SortedSet<string> families = new SortedSet<string>(familyQuery);

            ViewBag.FamilyList = families;

            IQueryable<string> genusQuery = from g in genusDb.Genus
                select g.Name;
            SortedSet<string> genera = new SortedSet<string>(genusQuery);

            ViewBag.GenusList = genera;

            IQueryable<string> speciesQuery = from g in speciesDb.Species
                select g.Name;
            SortedSet<string> species = new SortedSet<string>(speciesQuery);

            ViewBag.SpeciesList = species;

            return View();
        }

        // POST: SpeciesLabel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomGroup,Family,Genus,Species")] SpeciesLabel speciesLabel)
        {



            if (ModelState.IsValid)
            {
                db.SpeciesLabel.Add(speciesLabel);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View();
        }
    }
}