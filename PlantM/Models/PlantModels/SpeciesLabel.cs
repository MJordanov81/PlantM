using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PlantM.Models.PlantModels
{
    public class SpeciesLabel
    {
        [Key]
        public string Name { get; set; }

        public virtual CustomGroup CustomGroup { get; set; }

        [Required]
        public string CustomGroupName { get; set; }

        public virtual Family Family { get; set; }

        [Required]
        public string FamilyName { get; set; }

        public virtual Genus Genus { get; set; }

        [Required]
        public string GenusName { get; set; }

        public virtual Species Species { get; set; }

        [Required]
        public string SpeciesName { get; set; }

    }
}