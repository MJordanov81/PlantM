using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using PlantM.Models;
using PlantM.Models.PlantModels;

namespace PlantM.Controllers
{
    [Authorize]
    public class PlantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Plant
        public ActionResult Index()
        {
            var plant = db.Plant.Include(p => p.AcquisitionType).Include(p => p.Location).Include(p => p.Soil).Include(p => p.SpeciesLabel).Include(p => p.Vendor);
            return View(plant.ToList());
        }

        // GET: Plant/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plant.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // GET: Plant/Create
        public ActionResult Create()
        {
            ViewBag.AcquisitionTypeName = new SelectList(db.AcquisitionType, "Name", "Name");
            ViewBag.LocationName = new SelectList(db.Location, "Name", "Name");
            ViewBag.SoilName = new SelectList(db.Soil, "Name", "Name");
            ViewBag.SpeciesLabelName = new SelectList(db.SpeciesLabel, "Name", "Name");
            ViewBag.VendorName = new SelectList(db.Vendor, "Name", "Name");
            return View();
        }

        // POST: Plant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationName,SpeciesLabelName,Size,DateOfAcquisition,AcquisitionTypeName,VendorName,AgeAtAcquisition,SoilName,PotType,PhotoUrl,Comments")] Plant plant)
        {
            plant.CollectionNumber = GetCollectionNumber(plant);
            plant.DateOfAcquisitionAsDate = DateTime.Now;
            plant.LastRepottingDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Plant.Add(plant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AcquisitionTypeName = new SelectList(db.AcquisitionType, "Name", "Name", plant.AcquisitionTypeName);
            ViewBag.LocationName = new SelectList(db.Location, "Name", "Name", plant.LocationName);
            ViewBag.SoilName = new SelectList(db.Soil, "Name", "Name", plant.SoilName);
            ViewBag.SpeciesLabelName = new SelectList(db.SpeciesLabel, "Name", "Name", plant.SpeciesLabelName);
            ViewBag.VendorName = new SelectList(db.Vendor, "Name", "Name", plant.VendorName);
            return View(plant);
        }

        // GET: Plant/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plant.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcquisitionTypeName = new SelectList(db.AcquisitionType, "Name", "Name", plant.AcquisitionTypeName);
            ViewBag.LocationName = new SelectList(db.Location, "Name", "Name", plant.LocationName);
            ViewBag.SoilName = new SelectList(db.Soil, "Name", "Name", plant.SoilName);
            ViewBag.SpeciesLabelName = new SelectList(db.SpeciesLabel, "Name", "CustomGroupName", plant.SpeciesLabelName);
            ViewBag.VendorName = new SelectList(db.Vendor, "Name", "Name", plant.VendorName);
            return View(plant);
        }

        // POST: Plant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollectionNumber,LocationName,SpeciesLabelName,Size,DateOfAcquisition,AcquisitionTypeName,VendorName,AgeAtAcquisition,SoilName,PotType,PhotoUrl,Comments")] Plant plant)
        {
            plant.DateOfAcquisitionAsDate = db.Plant.Where(p => p.CollectionNumber == plant.CollectionNumber)
                .Select(p => p.DateOfAcquisitionAsDate).FirstOrDefault();

            plant.LastRepottingDate = db.Plant.Where(p => p.CollectionNumber == plant.CollectionNumber)
                .Select(p => p.LastRepottingDate).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AcquisitionTypeName = new SelectList(db.AcquisitionType, "Name", "Name", plant.AcquisitionTypeName);
            ViewBag.LocationName = new SelectList(db.Location, "Name", "Name", plant.LocationName);
            ViewBag.SoilName = new SelectList(db.Soil, "Name", "Name", plant.SoilName);
            ViewBag.SpeciesLabelName = new SelectList(db.SpeciesLabel, "Name", "Name", plant.SpeciesLabelName);
            ViewBag.VendorName = new SelectList(db.Vendor, "Name", "Name", plant.VendorName);
            return View(plant);
        }

        // GET: Plant/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plant.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: Plant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Plant plant = db.Plant.Find(id);
            db.Plant.Remove(plant);
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

        //GET: Plant/Repot/5
        public ActionResult Repot(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plant.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }

            ViewBag.SoilName = new SelectList(db.Soil, "Name", "Desription");
            return View(plant);
        }

        // POST: Plant/Repot/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Repot([Bind(Include = "CollectionNumber,SoilName,PotType")] Plant plantChanged)
        {
            Plant plant = db.Plant.FirstOrDefault(p => p.CollectionNumber == plantChanged.CollectionNumber);

            if (plant != null)
            {
                plant.LastRepottingDate = DateTime.Now;
                plant.PotType = plantChanged.PotType;
            }

            try
            {
                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.SoilName = new SelectList(db.Soil, "Name", "Desription");
                return View(plant);
            }        
        }

        //Returns plant's collection number depending on plant's CustomGroup
        private string GetCollectionNumber(Plant plant)
        {
            string customGroupName = db.SpeciesLabel
                .FirstOrDefault(s => s.Name == plant.SpeciesLabelName)
                .CustomGroupName;

            string customGroupFirstLetter = customGroupName[0].ToString();

            int customGroupPlantCount = 0;

            if (db.Plant != null)
            {
                customGroupPlantCount = db.Plant
                    .Count(p => p.CollectionNumber.StartsWith(customGroupFirstLetter));
            }

            return string.Format($"{customGroupName[0]}{customGroupName.Length}-{customGroupPlantCount + 1}");
        }
    }
}
