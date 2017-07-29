using System;
using PlantM.Models;
using PlantM.Models.PlantModels;
using System.Web.Mvc;

namespace PlantM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpeciesLabelController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: SpeciesLabel
        [HttpGet]
        public ActionResult Create(string confirmationMessage)
        {
            ViewBag.CustomGroupList = new SelectList(db.CustomGroup, "Name", "Name");
            ViewBag.FamilyList = new SelectList(db.Family, "Name", "Name");
            ViewBag.GenusList = new SelectList(db.Genus, "Name", "Name");
            ViewBag.SpeciesList = new SelectList(db.Species, "Name", "Name");


            ViewBag.Message = confirmationMessage;
            

            return View();
        }

        // POST: SpeciesLabel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomGroupName,FamilyName,GenusName,SpeciesName,FieldNumber")] SpeciesLabel speciesLabel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    if (speciesLabel.FieldNumber != null)
                    {
                        speciesLabel.Name =
                            string.Format(
                                $"{speciesLabel.GenusName} - {speciesLabel.SpeciesName} - {speciesLabel.FieldNumber}");
                    }
                    else
                    {
                        speciesLabel.Name = 
                            string.Format(
                                $"{speciesLabel.GenusName} - {speciesLabel.SpeciesName}");
                    }

                    db.SpeciesLabel.Add(speciesLabel);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Create", new { confirmationMessage = $"Label '{speciesLabel.Name}' has been created!" });
                    }

                    return RedirectToAction("Create", new { confirmationMessage = $"Label '{speciesLabel.Name}' has been created!" });
                }
            }

            ViewBag.CustomGroupList = new SelectList(db.CustomGroup, "Name", "Name");
            ViewBag.FamilyList = new SelectList(db.Family, "Name", "Name");
            ViewBag.GenusList = new SelectList(db.Genus, "Name", "Name");
            ViewBag.SpeciesList = new SelectList(db.Species, "Name", "Name");

            return View();
        }

        /*private List<SelectListItem> GetCustomGroups()
        {
            using (var db = new ApplicationDbContext())
            {
                List<SelectListItem> customGroups = new List<SelectListItem>();
                IQueryable<string> customGroupsQuery = from g in db.CustomGroup
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
        }

        private List<SelectListItem> GetFamilies()
        {
            using (var db = new ApplicationDbContext())
            {
                List<SelectListItem> families = new List<SelectListItem>();
                IQueryable<string> familiesQuery = from g in db.Family
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
        }

        private List<SelectListItem> GetGenera()
        {
            using (var db = new ApplicationDbContext())
            {
                List<SelectListItem> genera = new List<SelectListItem>();
                IQueryable<string> generaQuery = from g in db.Genus
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
        }

        private List<SelectListItem> GetSpecies()
        {
            using (var db = new ApplicationDbContext())
            {
                List<SelectListItem> species = new List<SelectListItem>();
                IQueryable<string> speciesQuery = from g in db.Species
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
        }*/
    }

/*    public class SpeciesLabelAttributes
    {
        public string CustomGroupName { get; set; }
        public string FamilyName { get; set; }
        public string GenusName { get; set; }
        public string SpeciesName { get; set; }
    }*/
}