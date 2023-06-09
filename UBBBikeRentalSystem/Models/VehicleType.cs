﻿using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.Models {
    public class VehicleType {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
