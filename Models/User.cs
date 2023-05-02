using Microsoft.AspNetCore.Identity;

namespace UBBBikeRentalSystem.Models {
    public class User : IdentityUser<string> {
        public DateTime CreatedAt;
    }
}
