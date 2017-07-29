using PagedList;
using PlantM.Models;
using PlantM.Models.PlantModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PlantM.Constants;

namespace PlantM.Controllers
{
    [Authorize]
    public class PlantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Plant
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CollectionNumberSortParam = String.IsNullOrEmpty(sortOrder) ? "colNum_desc" : "";
            ViewBag.LocationSortParam = sortOrder == "location_desc" ? "location_asc" : "location_desc";
            ViewBag.SpeciesLabelParam = sortOrder == "specLab_desc" ? "specLab_asc" : "specLab_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var plant = db.Plant.Include(p => p.AcquisitionType).Include(p => p.Location).Include(p => p.Soil).Include(p => p.SpeciesLabel).Include(p => p.Vendor);

            IQueryable<Plant> plantSorted;
            SortPlantList(sortOrder, plant, out plantSorted);

            List<Plant> plantFiltered;
            FilterPlantList(searchString, plantSorted, out plantFiltered);

            int pageSize = PlantConstants.PlantIndexPageSize;
            int pageNumber = (page ?? 1);
            return View(plantFiltered.ToPagedList(pageNumber, pageSize));
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CollectionNumber,Size,PhotoUrl,Comments")] Plant plantChanged)
        {
            Plant plant = db.Plant.FirstOrDefault(p => p.CollectionNumber == plantChanged.CollectionNumber);

            if (plant != null)
            {
                plant.Size = plantChanged.Size;
                plant.PhotoUrl = plantChanged.PhotoUrl;
                plant.Comments = plantChanged.Comments;
            }

            try
            {
                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.AcquisitionTypeName = new SelectList(db.AcquisitionType, "Name", "Name", plant.AcquisitionTypeName);
                ViewBag.LocationName = new SelectList(db.Location, "Name", "Name", plant.LocationName);
                ViewBag.SoilName = new SelectList(db.Soil, "Name", "Name", plant.SoilName);
                ViewBag.SpeciesLabelName = new SelectList(db.SpeciesLabel, "Name", "Name", plant.SpeciesLabelName);
                ViewBag.VendorName = new SelectList(db.Vendor, "Name", "Name", plant.VendorName);
                return View(plant);
            }
        }

        // GET: Plant/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            Plant plant = db.Plant.Find(id);

            if (plant != null)
            {
                plant.DeletionDate = DateTime.Now;
                plant.IsDeleted = true;

                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        private string GetCollectionNumber(Plant plant)
        {
            string customGroupName = db.SpeciesLabel
                .FirstOrDefault(s => s.Name == plant.SpeciesLabelName)
                .CustomGroupName;

            string customGroupFirstLetter = customGroupName[0].ToString();
            string customGroupNumberOfLetters = customGroupName.Length.ToString();
            string customGroup = customGroupFirstLetter + customGroupNumberOfLetters;

            int customGroupPlantCount = 0;

            if (db.Plant != null)
            {
                customGroupPlantCount = db.Plant
                    .Count(p => p.CollectionNumber.StartsWith(customGroup));
            }

            return string.Format($"{customGroup}-{customGroupPlantCount + 1}");
        }

        //GET: Plant/Wither/5
        [Authorize(Roles = "Admin")]
        public ActionResult Wither(string id)
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

        // POST: Plant/Wither/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Wither([Bind(Include = "CollectionNumber,WitherReason")] Plant plantChanged)
        {
            Plant plant = db.Plant.FirstOrDefault(p => p.CollectionNumber == plantChanged.CollectionNumber);

            if (plant != null)
            {
                plant.WitherReason = plantChanged.WitherReason;
                plant.HasWithered = true;
                plant.WitheredDate = DateTime.Now;
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

        //GET: Plant/Relocate/5
        [Authorize(Roles = "Admin")]
        public ActionResult Relocate(string id)
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

            ViewBag.LocationName = new SelectList(db.Location, "Name", "Name");
            return View(plant);
        }

        // POST: Plant/Relocate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Relocate([Bind(Include = "CollectionNumber,LocationName")] Plant plantChanged)
        {
            Plant plant = db.Plant.FirstOrDefault(p => p.CollectionNumber == plantChanged.CollectionNumber);

            if (plant != null)
            {
                plant.LocationName = plantChanged.LocationName;
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
        private void SortPlantList(string sortOrder, IQueryable<Plant> plant, out IQueryable<Plant> plantSorted)
        {
            switch (sortOrder)
            {
                case "colNum_desc":
                    plantSorted = plant.OrderBy(p => p.CollectionNumber);
                    break;
                case "location_asc":
                    plantSorted = plant.OrderByDescending(p => p.LocationName);
                    break;
                case "location_desc":
                    plantSorted = plant.OrderBy(p => p.LocationName);
                    break;
                case "specLab_asc":
                    plantSorted = plant.OrderByDescending(p => p.SpeciesLabelName);
                    break;
                case "specLab_desc":
                    plantSorted = plant.OrderBy(p => p.SpeciesLabelName);
                    break;
                default:
                    plantSorted = plant.OrderByDescending(p => p.CollectionNumber);
                    break;
            }
        }

        private void FilterPlantList(string searchString, IQueryable<Plant> plantSorted, out List<Plant> plantFiltered)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                plantFiltered = new List<Plant>(plantSorted);
                return;
            }
            if (searchString.ToLower() == "withered")
            {
                plantFiltered = plantSorted.Where(p => p.HasWithered == true).ToList();
                return;
            }
            if (searchString.ToLower() == "deleted")
            {
                plantFiltered = plantSorted.Where(p => p.IsDeleted == true).ToList();
                return;
            }
            if (searchString.ToLower() == "alive")
            {
                plantFiltered = plantSorted.Where(p => p.IsDeleted == false && p.HasWithered == false).ToList();
                return;
            }

            HashSet<Plant> plants = new HashSet<Plant>();

            searchString = searchString.ToLower();

            plants.UnionWith(plantSorted.Where(p => p.CollectionNumber.ToLower().Contains(searchString)));
            plants.UnionWith(plantSorted.Where(p => p.AcquisitionTypeName.ToLower().Contains(searchString)));
            plants.UnionWith(plantSorted.Where(p => p.LocationName.ToLower().Contains(searchString)));
            plants.UnionWith(plantSorted.Where(p => p.SoilName.ToLower().Contains(searchString)));
            plants.UnionWith(plantSorted.Where(p => p.SpeciesLabelName.ToLower().Contains(searchString)));
            plants.UnionWith(plantSorted.Where(p => p.VendorName.ToLower().Contains(searchString)));
            plants.UnionWith(plantSorted.Where(p => p.DateOfAcquisition.ToLower().Contains(searchString)));
            plants.UnionWith(plantSorted.Where(p => p.PotType.ToLower().Contains(searchString)));

            plantFiltered = new List<Plant>(plants);
        }
    }
}
