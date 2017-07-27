using PlantM.Models.PlantModels;
using System.Web.Mvc;
using PlantM.Models;

namespace PlantM.Controllers
{
    [Authorize]
    public class FamilyController : Controller
    {
/*        FamilyDbContext db = new FamilyDbContext();*/

        // GET: Family
        public ActionResult Create()
        {
            return View();
        }

        // POST: Family/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Family family)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Family.Add(family);
                    db.SaveChanges();
                    return RedirectToAction("Create", "SpeciesLabel", new { confirmationMessage = $"Family {family.Name} has been created!" });
                }
            }

            return View();
        }
    }
}