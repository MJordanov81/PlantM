using System.Web.Mvc;
using PlantM.Models.PlantModels;

namespace PlantM.Controllers
{
    public class GenusController : Controller
    {
        GenusDbContext db = new GenusDbContext();

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
                db.Genus.Add(genus);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View();
        }
    }
}