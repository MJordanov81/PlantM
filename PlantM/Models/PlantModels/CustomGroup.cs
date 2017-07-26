using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PlantM.Models.PlantModels
{
    public class CustomGroup
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }

    public class CustomGroupDbContext : DbContext
    {
        public CustomGroupDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<CustomGroup> CustomGroup { get; set; }
    }
}