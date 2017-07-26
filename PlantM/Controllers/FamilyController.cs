using PlantM.Models.PlantModels;
using System.Web.Mvc;

namespace PlantM.Controllers
{
    public class FamilyController : Controller
    {
        FamilyDbContext db = new FamilyDbContext();

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
                db.Family.Add(family);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View();
        }
    }
}