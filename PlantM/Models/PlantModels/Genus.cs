using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PlantM.Models.PlantModels
{
    public class Genus
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }

    public class GenusDbContext : DbContext
    {
        public GenusDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Genus> Genus { get; set; }
    }
}