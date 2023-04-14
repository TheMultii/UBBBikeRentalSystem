using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Database {
    public class UBBBikeRentalSystemDatabase : IdentityDbContext<User, IdentityRole<int>, int> {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public override DbSet<User> Users { get; set; }
        public DbSet<ReservationPoint> ReservationPoints { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseInMemoryDatabase("UBBBikeRentalSystemDatabase");
        }
    }
}
