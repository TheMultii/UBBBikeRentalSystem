using System.ComponentModel.DataAnnotations;

namespace UBBBikeRentalSystem.Models {
    public class User {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
