using FluentValidation;
using FluentValidation.AspNetCore;
using UBBBikeRentalSystem.Converters;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;
using UBBBikeRentalSystem.Services;
using UBBBikeRentalSystem.Validators;
using UBBBikeRentalSystem.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace UBBBikeRentalSystem {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Register the database context.
            builder.Services.AddDbContext<UBBBikeRentalSystemDatabase>();

            builder.Services.AddDefaultIdentity<User>(
                //options => options.SignIn.RequireConfirmedAccount = true
                )
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UBBBikeRentalSystemDatabase>();
            builder.Services.AddScoped<IRepository<ReservationPoint, int>, ReservationPointRepository>();
            builder.Services.AddScoped<IRepository<Reservation, int>, ReservationRepository>();
            builder.Services.AddScoped<IRepository<User, string>, UserRepository>();
            builder.Services.AddScoped<IRepository<VehicleType, int>, VehicleTypeRepository>();
            builder.Services.AddScoped<IRepository<Vehicle, int>, VehicleRepository>();

            // Add FluentValidation services to the container.
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<ReservationPointValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<ReservationValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<VehicleTypeValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<VehicleValidator>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
            });

            var app = builder.Build();

            // Get a reference to the database context
            using (var serviceScope = app.Services.CreateScope()) {
                var dbContext = serviceScope.ServiceProvider.GetService<UBBBikeRentalSystemDatabase>();
                if (dbContext is null) throw new Exception("dbContext is null");

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

            //Get a reference to the UserManager
            using (var serviceScope = app.Services.CreateScope()) {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                if (userManager is null || roleManager is null) throw new Exception("userManager or/and roleManager is null");

                //create default roles
                var roles = new[] { "Administrator", "Operator", "Użytkownik" };
                foreach (var role in roles) {
                    if (!roleManager.RoleExistsAsync(role).Result) {
                        var result = roleManager.CreateAsync(new IdentityRole(role)).Result;
                        if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
                    }
                }

                //create default users
                var user_admin = new User {
                    UserName = "Administrator",
                    Email = "administrator@ubb.bielsko.pl",
                    CreatedAt = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                };
                userManager.CreateAsync(user_admin, "Admin123!").Wait();
                userManager.AddToRoleAsync(user_admin, "Administrator").Wait();

                var user_operator = new User {
                    UserName = "Operator",
                    Email = "operator@ubb.bielsko.pl",
                    CreatedAt = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                };
                userManager.CreateAsync(user_operator, "Operator123!").Wait();
                userManager.AddToRoleAsync(user_operator, "Operator").Wait();

                var user_user = new User {
                    UserName = "User",
                    Email = "user@ubb.bielsko.pl",
                    CreatedAt = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                };
                userManager.CreateAsync(user_user, "User123!").Wait();
                userManager.AddToRoleAsync(user_user, "Użytkownik").Wait();
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

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "Admin",
                areaName: "Admin",
                pattern: "Admin/{action=Index}/{id?}",
                defaults: new { controller = "Admin" });

            app.Run();
        }

    }
}