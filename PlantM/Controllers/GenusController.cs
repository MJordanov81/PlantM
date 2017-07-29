using System.Web.Mvc;
using PlantM.Models;
using PlantM.Models.PlantModels;

namespace PlantM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenusController : Controller
    {
/*        GenusDbContext db = new GenusDbContext();*/

        // GET: Genus
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Genus genus)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Genus.Add(genus);
                    db.SaveChanges();
                    return RedirectToAction("Create", "SpeciesLabel", new { confirmationMessage = $"Genus {genus.Name} has been created!" });
                }
            }

            return View();
        }
    }
}