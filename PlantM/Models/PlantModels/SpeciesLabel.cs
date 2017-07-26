using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PlantM.Models.PlantModels
{
    public class SpeciesLabel
    {
        [Key]
        [Required]
        public string Name { get; set; }

        [Required]
        public CustomGroup CustomGroup { get; set; }

        [Required]
        public Family Family { get; set; }

        [Required]
        public Genus Genus { get; set; }

        [Required]
        public Species Species { get; set; }
    }

    public class SpeciesLabelDbContext : DbContext
    {
        public SpeciesLabelDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<SpeciesLabel> SpeciesLabel { get; set; }
    }
}