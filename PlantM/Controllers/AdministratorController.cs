using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PlantM.Constants;
using PlantM.Models;
using PlantM.Models.PlantModels;

namespace PlantM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrator/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Administrator/TestEntities
        public ActionResult CreateTestEntities()
        {
            Plant testPlant = db.Plant.FirstOrDefault(p => p.CollectionNumber == "TestPlant1");

            if (testPlant == null)
            {
                Location testLocation1 = new Location();
                testLocation1.Name = "Sofia";

                AcquisitionType testAcquisitionType1 = new AcquisitionType();
                testAcquisitionType1.Name = "Purchase";

                Vendor testVendor1 = new Vendor();
                testVendor1.Name = "Vendor One";
                testVendor1.WebSite = "www.testVendorOne.com";

                Soil testSoil1 = new Soil();
                testSoil1.Name = "MixOne";
                testSoil1.Desription = "100% mineral soil";

                CustomGroup testCustomGroup1 = new CustomGroup();
                testCustomGroup1.Name = "Test group";

                Family testFamily1 = new Family();
                testFamily1.Name = "Test family";

                Genus testGenus1= new Genus();
                testGenus1.Name = "Test genus";

                Species testSpecies1 = new Species();
                testSpecies1.Name = "Test species";

                SpeciesLabel testSpeciesLabel1 = new SpeciesLabel();
                testSpeciesLabel1.CustomGroupName = testCustomGroup1.Name;
                testSpeciesLabel1.FamilyName = testFamily1.Name;
                testSpeciesLabel1.GenusName = testGenus1.Name;
                testSpeciesLabel1.SpeciesName = testSpecies1.Name;
                testSpeciesLabel1.FieldNumber = "T111";
                testSpeciesLabel1.Name = string.Format($"{testGenus1.Name} - {testSpecies1.Name} - {testSpeciesLabel1.FieldNumber}");

                Plant testPlant1 = new Plant();
                testPlant1.LocationName = testLocation1.Name;
                testPlant1.AcquisitionTypeName = testAcquisitionType1.Name;
                testPlant1.VendorName = testVendor1.Name;
                testPlant1.SoilName = testSoil1.Name;
                testPlant1.SpeciesLabelName = testSpeciesLabel1.Name;
                testPlant1.CollectionNumber = "TestPlant1";
                testPlant1.AgeAtAcquisition = 3;
                testPlant1.DateOfAcquisition = DateTime.Now.ToString();
                testPlant1.DateOfAcquisitionAsDate = DateTime.Now;
                testPlant1.PhotoUrl = "https://lh3.googleusercontent.com/YB8cadkkshAtrd8ikroC3hY_NMMsx2E5TXgqazjjIllsGwE8TQmBPnWUOWbIMW8kWCR_cSYmpamJ2H1Lz5bv1Fj_Z_51um26yZpiQwhmgmCWBethL6Mq8koOLsadL9Vsygiu2LnJ";
                testPlant1.Comments = "This plant is a test plant.";
                testPlant1.LastRepottingDate = DateTime.Now;
                testPlant1.PotType = "3cm, round";
                testPlant1.Size = "3cm high";

                db.Location.Add(testLocation1);
                db.AcquisitionType.Add(testAcquisitionType1);
                db.Vendor.Add(testVendor1);
                db.Soil.Add(testSoil1);
                db.CustomGroup.Add(testCustomGroup1);
                db.Family.Add(testFamily1);
                db.Genus.Add(testGenus1);
                db.Species.Add(testSpecies1);
                db.SpeciesLabel.Add(testSpeciesLabel1);
                db.Plant.Add(testPlant1);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    ViewBag.Message = "Problem encountered with creating test entities!";
                    return View("Index");
                }

                ViewBag.Message = "Test entities have been created!";
                return View("Index");

            }

            ViewBag.Message = "Test entities have already been created!";
            return View("Index");
        }

        // GET: Administrator/ExportPlants
        public ActionResult ExportPlants()
        {
            ExportPlantsData();

            return RedirectToAction("Index", "Administrator");
        }

        public void ExportPlantsData()
        {
            var plants = db.Plant.ToList();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(PlantConstants.ExportHeader);
            foreach (var plant in plants)
            {
                sb.AppendLine(plant.ToString());
            }

            Response.ClearContent();
            Response.AddHeader("content-disposition", $"attachment;filename=plantDbExport" + $"{DateTime.Now:yyyy-MM-dd}" + ".txt");
            Response.ContentType = "text/csv";
            Response.Write(sb.ToString());
            Response.End();
        }

        // GET: Administrator/EmptyTable
        public ActionResult EmptyTable()
        {
            ViewBag.Tables = GetDbTablesNames();
            return View();
        }

        // POST: Administrator/EmptyTable
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmptyTable(string tables)
        {

            ViewBag.Message = EmptyTableByName(tables);
            ViewBag.Tables = GetDbTablesNames();
            return View();
        }

        private string EmptyTableByName(string tables)
        {
            string message = $"Table {tables} has been succesfully emptied!";
            try
            {
                db.Database.ExecuteSqlCommand($"DELETE FROM {tables}");
            }
            catch (Exception e)
            {
                message = e.Message;

            }


            return message;
        }

        private SelectList GetDbTablesNames()
        {
            Dictionary<string, string> tables = new Dictionary<string, string>();

            foreach (var table in Enum.GetNames(typeof(DbConstants.DbTables)))
            {
                tables.Add(table, table);
            }

            return new SelectList(tables, "Key", "Value");
        }
    }
}