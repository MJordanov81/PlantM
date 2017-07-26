using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            ViewBag.CustomGroupList = GetCustomGroups();
            ViewBag.FamilyList = GetFamilies();
            ViewBag.GenusList = GetGenera();
            ViewBag.SpeciesList = GetSpecies();

            return View();
        }

        // POST: SpeciesLabel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,CustomGroup,Family,Genus,Species")] SpeciesLabel speciesLabel)
        {
            if (ModelState.IsValid)
            {
                db.SpeciesLabel.Add(speciesLabel);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.CustomGroupList = GetCustomGroups();
            ViewBag.FamilyList = GetFamilies();
            ViewBag.GenusList = GetGenera();
            ViewBag.SpeciesList = GetSpecies();

            return View();
        }

        private List<SelectListItem> GetCustomGroups()
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

            return customGroups;
        }

        private List<SelectListItem> GetFamilies()
        {
            List<SelectListItem> families = new List<SelectListItem>();
            IQueryable<string> familiesQuery = from g in familyDb.Family
                select g.Name;

            foreach (var element in familiesQuery)
            {
                families.Add(new SelectListItem()
                {
                    Value = element,
                    Text = element
                });
            }

            return families;
        }

        private List<SelectListItem> GetGenera()
        {
            List<SelectListItem> genera = new List<SelectListItem>();
            IQueryable<string> generaQuery = from g in genusDb.Genus
                select g.Name;

            foreach (var element in generaQuery)
            {
                genera.Add(new SelectListItem()
                {
                    Value = element,
                    Text = element
                });
            }

            return genera;
        }

        private List<SelectListItem> GetSpecies()
        {
            List<SelectListItem> species = new List<SelectListItem>();
            IQueryable<string> speciesQuery = from g in speciesDb.Species
                select g.Name;

            foreach (var element in speciesQuery)
            {
                species.Add(new SelectListItem()
                {
                    Value = element,
                    Text = element
                });
            }

            return species;
        }
    }
}