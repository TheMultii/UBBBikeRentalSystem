using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class VehicleRepository: IRepository<Vehicle> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public VehicleRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public List<Vehicle> GetAll() {
            return _db.Vehicles.Include(r => r.VehicleType).OrderBy(r => r.ID).ToList();
        }

        public Vehicle? Get(int id) {
            return _db.Vehicles.Include(r => r.VehicleType).SingleOrDefault(r => r.ID == id);
        }

        public void Delete(int id) {
            var _delete = Get(id) ?? throw new Exception("Brak takiego pojazdu w DB");
            _db.Vehicles.Remove(_delete);
            _db.SaveChanges();
        }

        public void Update(Vehicle vehicle) {
            var _oldVehicle = Get(vehicle.ID) ?? throw new Exception("Brak takiego pojazdu w DB");
            _db.Entry(_oldVehicle).State = EntityState.Detached;
            _db.Vehicles.Update(vehicle);
            _db.SaveChanges();
        }

        public void Add(Vehicle vehicle) {
            vehicle.ID = _db.Vehicles.Max(r => r.ID) + 1;
            _db.Vehicles.Add(vehicle);
            _db.SaveChanges();
        }

        public void AddRange(IEnumerable<Vehicle> entities) {
            int currentID = _db.Vehicles.Max(r => r.ID) + 1;
            foreach (var _vehicle in entities) {
                _vehicle.ID = currentID;
                _db.Vehicles.Add(_vehicle);
                _db.SaveChanges();
                currentID++;
            }
        }

        public IQueryable<Vehicle> RawQueryable() {
            return _db.Vehicles.AsQueryable();
        }
    }
}
