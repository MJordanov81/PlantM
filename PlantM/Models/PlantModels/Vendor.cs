using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantM.Models.PlantModels
{
    public class Vendor
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Web site")]
        public string WebSite { get; set; }
    }
}