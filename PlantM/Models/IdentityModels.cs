using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlantM.Models.PlantModels;

namespace PlantM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

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