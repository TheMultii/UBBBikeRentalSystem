using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Database {
    public class UBBBikeRentalSystemDatabase : IdentityDbContext<Models.User, IdentityRole<int>, int> {
        public DbSet<Models.Vehicle> Vehicles { get; set; }
        public DbSet<Models.Reservation> Reservations { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.ReservationPoint> ReservationPoints { get; set; }
        public DbSet<Models.VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseInMemoryDatabase("UBBBikeRentalSystemDatabase");
        }
    }
}
