using PlantM.Models.PlantModels;
using System.Web.Mvc;
using PlantM.Models;

namespace PlantM.Controllers
{
    [Authorize]
    public class CustomGroupController : Controller
    {
/*        private CustomGroupDbContext db = new CustomGroupDbContext();*/

        // GET: CustomGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomGroup/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] CustomGroup customGroup)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.CustomGroup.Add(customGroup);
                    db.SaveChanges();
                    return RedirectToAction("Create", "SpeciesLabel", new { confirmationMessage = $"Custom group {customGroup.Name} has been created!" });
                }
            }

            return View();
        }
    }
}