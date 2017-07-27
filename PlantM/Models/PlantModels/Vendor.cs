﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantM.Models.PlantModels
{
    public class Vendor
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public string WebSite { get; set; }
    }
}