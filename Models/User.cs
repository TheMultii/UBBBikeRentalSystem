﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.Models {
    public class User : IdentityUser {
        public int ID { get; set; }
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }
    }
}
