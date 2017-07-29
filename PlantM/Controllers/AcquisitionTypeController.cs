using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlantM.Models;
using PlantM.Models.Exceptions;
using PlantM.Models.PlantModels;

namespace PlantM.Controllers
{
    [Authorize]
    public class AcquisitionTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AcquisitionType
        public ActionResult Index()
        {
            return View(db.AcquisitionType.ToList());
        }

        // GET: AcquisitionType/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AcquisitionType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Name")] AcquisitionType acquisitionType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.AcquisitionType.Add(acquisitionType);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }

                return RedirectToAction("Index");
                  
            }

            return View(acquisitionType);
        }

        // GET: AcquisitionType/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcquisitionType acquisitionType = db.AcquisitionType.Find(id);
            if (acquisitionType == null)
            {
                return HttpNotFound();
            }

            return View(acquisitionType);
        }

        // POST: AcquisitionType/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AcquisitionType acquisitionType = db.AcquisitionType.Find(id);

            bool AreThereDependantPlants = db.Plant.Any(plant => plant.AcquisitionTypeName == acquisitionType.Name);

            if (AreThereDependantPlants)
            {
                ViewBag.ErrorMessage = "Cannot delete because this acquisition type is already in use!";

                return View(acquisitionType);
            }

            db.AcquisitionType.Remove(acquisitionType);
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
