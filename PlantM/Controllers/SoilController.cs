using PlantM.Models;
using PlantM.Models.PlantModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PlantM.Controllers
{
    [Authorize]
    public class SoilController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Soil
        public ActionResult Index()
        {
            return View(db.Soil.ToList());
        }

        // GET: Soil/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soil soil = db.Soil.Find(id);
            if (soil == null)
            {
                return HttpNotFound();
            }
            return View(soil);
        }

        // GET: Soil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Soil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Desription")] Soil soil)
        {
            if (ModelState.IsValid)
            {
                db.Soil.Add(soil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(soil);
        }

        // GET: Soil/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soil soil = db.Soil.Find(id);
            if (soil == null)
            {
                return HttpNotFound();
            }
            return View(soil);
        }

        // POST: Soil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Desription")] Soil soil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(soil);
        }

        // GET: Soil/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soil soil = db.Soil.Find(id);
            if (soil == null)
            {
                return HttpNotFound();
            }
            return View(soil);
        }

        // POST: Soil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Soil soil = db.Soil.Find(id);

            bool AreThereDependantPlants = db.Plant.Any(plant => plant.SoilName == soil.Name);

            if (AreThereDependantPlants)
            {
                ViewBag.ErrorMessage = "Cannot delete because this location is already in use!";

                return View(soil);
            }

            db.Soil.Remove(soil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
