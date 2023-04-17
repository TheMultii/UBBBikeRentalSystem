using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class VehicleTypeRepository: IRepository<VehicleType, int> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public VehicleTypeRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public List<VehicleType> GetAll() {
            return _db.VehicleTypes.OrderBy(r => r.ID).ToList();
        }

        public VehicleType? Get(int id) {
            return _db.VehicleTypes.OrderBy(r => r.ID).SingleOrDefault(r => r.ID == id);
        }

        public void Delete(int id) {
            var _delete = Get(id) ?? throw new Exception("Brak takiego typu pojazdu w DB");
            _db.VehicleTypes.Remove(_delete);
            _db.SaveChanges();
        }

        public void Update(VehicleType vehicleType) {
            var _oldVehicleType = Get(vehicleType.ID) ?? throw new Exception("Brak takiego typu pojazdu w DB");
            _db.Entry(_oldVehicleType).CurrentValues.SetValues(vehicleType);
            _db.SaveChanges();
        }

        public void Add(VehicleType vehicleType) {
            vehicleType.ID = _db.VehicleTypes.OrderBy(r => r.ID).First().ID + 1;
            _db.VehicleTypes.Add(vehicleType);
            _db.SaveChanges();
        }

        public void AddRange(IEnumerable<VehicleType> entities) {
            int currentID = _db.VehicleTypes.OrderBy(r => r.ID).First().ID + 1;
            foreach (var _vehicle in entities) {
                _vehicle.ID = currentID;
                _db.VehicleTypes.Add(_vehicle);
                _db.SaveChanges();
                currentID++;
            }
        }

        public IQueryable<VehicleType> RawQueryable() {
            return _db.VehicleTypes.AsQueryable();
        }
    }
}
