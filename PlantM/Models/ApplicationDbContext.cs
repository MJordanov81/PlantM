using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using PlantM.Models.PlantModels;

namespace PlantM.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<CustomGroup> CustomGroup { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<Genus> Genus { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<SpeciesLabel> SpeciesLabel { get; set; }
        public DbSet<Soil> Soil { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<AcquisitionType> AcquisitionType { get; set; }
        public DbSet<Plant> Plant { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}