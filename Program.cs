using UBBBikeRentalSystem.Converters;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.ViewModels;

namespace UBBBikeRentalSystem {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Register the database context.
            builder.Services.AddDbContext<UBBBikeRentalSystemDatabase>();
            builder.Services.AddScoped<IRepository<Vehicle>, VehicleRepository>();
            builder.Services.AddScoped<IRepository<VehicleType>, VehicleTypeRepository>();
            builder.Services.AddScoped<IRepository<ReservationPoint>, ReservationPointRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Get a reference to the database context
            using (var serviceScope = app.Services.CreateScope()) {
                //add here sample

                var dbContext = serviceScope.ServiceProvider.GetService<UBBBikeRentalSystemDatabase>();
                if (dbContext is null) return;

                List<VehicleDetailViewModel> veh_list = new() {
                    new() {
                        ID = 1,
                        Name = "Rower 1",
                        Model = "Model 1",
                        ImageUrl = "/img/r1.jpg",
                        VehicleType = VehicleTypeEnum.Bike,
                        Description = "Klasyczny rower miejski",
                        IsAvailable = true,
                        ManufactureDate = DateTime.Parse("2021-01-01"),
                        MaxSpeed = 15,
                        PricePerHour = 10
                    },
                    new() {
                        ID = 2,
                        Name = "Rower 2",
                        Model = "Model 2",
                        ImageUrl = "/img/r2.jpg",
                        VehicleType = VehicleTypeEnum.Bike,
                        Description = "Rower górski z amortyzatorem",
                        IsAvailable = false,
                        ManufactureDate = DateTime.Parse("2020-05-01"),
                        MaxSpeed = 25,
                        PricePerHour = 15
                    },
                    new() {
                        ID = 3,
                        Name = "Samochód 1",
                        Model = "Model 3",
                        ImageUrl = "/img/car.jpg",
                        VehicleType = VehicleTypeEnum.Car,
                        Description = "Samochód klasy premium",
                        IsAvailable = true,
                        ManufactureDate = DateTime.Parse("2019-08-01"),
                        MaxSpeed = 220,
                        PricePerHour = 100
                    },
                    new() {
                        ID = 4,
                        Name = "Hulajnoga 1",
                        Model = "Model 4",
                        ImageUrl = "/img/scooter.jpg",
                        VehicleType = VehicleTypeEnum.Scooter,
                        Description = "Hulajnoga elektryczna",
                        IsAvailable = true,
                        ManufactureDate = DateTime.Parse("2021-02-01"),
                        MaxSpeed = 20,
                        PricePerHour = 5
                    }
                };

                List<VehicleType> vehicleTypes = new() {
                    new() {
                        ID = 1,
                        Name = "Bike",
                        Description = "Klasyczny rower miejski"
                    },
                    new() {
                        ID = 2,
                        Name = "Scooter",
                        Description = "Hulajnoga elektryczna"
                    },
                    new() {
                        ID = 3,
                        Name = "Car",
                        Description = "Samochód klasy premium"
                    }
                };

                dbContext.VehicleTypes.AddRange(vehicleTypes);
                dbContext.SaveChanges();

                foreach (var _veh in veh_list) {
                    Vehicle vehicle = new() {
                        ID = _veh.ID,
                        Name = _veh.Name,
                        Model = _veh.Model,
                        ImageUrl = _veh.ImageUrl,
                        VehicleType = VehicleTypeConverter.ToVehicleType(_veh.VehicleType, dbContext),
                        Description = _veh.Description,
                        IsAvailable = _veh.IsAvailable,
                        ManufactureDate = _veh.ManufactureDate,
                        MaxSpeed = _veh.MaxSpeed,
                        PricePerHour = _veh.PricePerHour
                    };
                    dbContext.Vehicles.Add(vehicle);
                }
                dbContext.SaveChanges();

                for (int i = 1; i <= 10; i++) {
                    dbContext.Users.Add(new() { ID = i, Name="ASD" });
                }
                dbContext.SaveChanges();

                List<ReservationPoint> _reservationPoints = new() {
                    new() {
                        ID = 1,
                        Name = "A",
                        Address = "Ulicaa",
                        City = "BB",
                        PostalCode = "12-345",
                        Longitude = 12.133,
                        Latitude = 13.122
                    },
                    new() {
                        ID = 2,
                        Name = "B",
                        Address = "ctyvb",
                        City = "CC",
                        PostalCode = "54-321",
                        Longitude = 1.111,
                        Latitude = 2.222
                    }
                };

                dbContext.ReservationPoints.AddRange(_reservationPoints);
                dbContext.SaveChanges();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

    }
}