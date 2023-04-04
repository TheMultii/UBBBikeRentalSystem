using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem.Database {
    public class UBBBikeRentalSystemDatabase : DbContext {
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
