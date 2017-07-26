using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PlantM.Models.PlantModels
{
    public class Family
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }

    public class FamilyDbContext : DbContext
    {
        public FamilyDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Family> Family { get; set; }
    }
}