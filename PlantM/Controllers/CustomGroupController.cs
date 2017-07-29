using System;
using PlantM.Models.PlantModels;
using System.Web.Mvc;
using PlantM.Models;

namespace PlantM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomGroupController : Controller
    {

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
                    try
                    {
                        db.CustomGroup.Add(customGroup);
                        db.SaveChanges();
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        return RedirectToAction("Create", "SpeciesLabel",
                            new {confirmationMessage = $"Custom group {customGroup.Name} couldn't be created probably because it already exists!" + Environment.NewLine + $"{e.Message}"});
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Create", "SpeciesLabel", 
                            new { confirmationMessage = $"Custom group {customGroup.Name} couldn't be created due to this exception: '{e.Message}' "});
                    }

                    return RedirectToAction("Create", "SpeciesLabel", 
                        new { confirmationMessage = $"Custom group {customGroup.Name} has been created!" });
                }
            }

            return View();
        }
    }
}