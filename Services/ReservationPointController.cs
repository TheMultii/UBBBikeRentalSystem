using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class ReservationPointController : IRepository<ReservationPoint> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public ReservationPointController(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public void Add(ReservationPoint entity) {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<ReservationPoint> entities) {
            throw new NotImplementedException();
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public ReservationPoint? Get(int id) {
            throw new NotImplementedException();
        }

        public List<ReservationPoint> GetAll() {
            throw new NotImplementedException();
        }

        public IQueryable<ReservationPoint> RawQueryable() {
            return _db.ReservationPoints.AsQueryable();
        }

        public void Update(ReservationPoint entity) {
            throw new NotImplementedException();
        }
    }
}
