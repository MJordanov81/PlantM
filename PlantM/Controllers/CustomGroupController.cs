using PlantM.Models.PlantModels;
using System.Web.Mvc;

namespace PlantM.Controllers
{
    public class CustomGroupController : Controller
    {
        private CustomGroupDbContext db = new CustomGroupDbContext();

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
                db.CustomGroup.Add(customGroup);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View();
        }
    }
}