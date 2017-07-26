using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PlantM.Models.PlantModels
{
    public class Species
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }

    public class SpeciesDbContext : DbContext
    {
        public SpeciesDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Species> Species { get; set; }
    }
}