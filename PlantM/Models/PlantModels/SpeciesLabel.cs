using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PlantM.Models.PlantModels
{
    public class SpeciesLabel
    {
        [Key]
        public string Name { get; set; }

        public string FieldNumber { get; set; }

        [Required]
        public string CustomGroupName { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        public string GenusName { get; set; }

        [Required]
        public string SpeciesName { get; set; }

        public virtual CustomGroup CustomGroup { get; set; }
        public virtual Family Family { get; set; }
        public virtual Genus Genus { get; set; }
        public virtual Species Species { get; set; }
    }
}