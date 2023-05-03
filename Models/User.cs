using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.Models {
    public class User : IdentityUser<string> {
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedAt { get; set; }
    }
}
