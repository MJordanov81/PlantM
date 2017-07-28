using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantM.Models.PlantModels
{
    public class Vendor
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Web site")]
        public string Name { get; set; }
        public string WebSite { get; set; }
    }
}