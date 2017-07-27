using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PlantM.Models.PlantModels
{
    public class Plant
    {
        [Key]
        [Display(Name = "Collection No.")]
        public string CollectionNumber { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Required]
        [Display(Name = "Species")]
        public string SpeciesLabelName { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        [Display(Name = "Acquired on")]
        [DataType(DataType.Date)]
        public string DateOfAcquisition { get; set; }

        [Required]
        public DateTime DateOfAcquisitionAsDate { get; set; }

        [Required]
        [Display(Name = "Last repotting on")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime LastRepottingDate { get; set; }

        [Required]
        [Display(Name = "Acquired by")]
        public string AcquisitionTypeName { get; set; }

        [Required]
        [Display(Name = "Vendor")]
        public string VendorName { get; set; }

        [Required]
        [Display(Name = "Age at acquisition")]
        public int AgeAtAcquisition { get; set; }

        [Display(Name = "Age")]
        [DisplayFormat(DataFormatString = "{0:f1}")]
        public double Age {
            get { return (DateTime.Now - this.DateOfAcquisitionAsDate).TotalDays / 365 + this.AgeAtAcquisition; }
        }

        [Required]
        [Display(Name = "Soil")]
        public string SoilName { get; set; }

        [Required]
        [Display(Name = "Pot type")]
        public string PotType { get; set; }

        [Required]
        [Display(Name = "Photo URL")]
        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; }

        [Required]
        public string Comments { get; set; }

        public virtual Location Location { get; set; }
        public virtual SpeciesLabel SpeciesLabel { get; set; }
        public virtual AcquisitionType AcquisitionType { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Soil Soil { get; set; }
    }
}