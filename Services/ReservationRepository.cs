using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class ReservationRepository : IRepository<Reservation> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public ReservationRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public void Add(Reservation entity) {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Reservation> entities) {
            throw new NotImplementedException();
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public Reservation? Get(int id) {
            throw new NotImplementedException();
        }

        public List<Reservation> GetAll() {
            throw new NotImplementedException();
        }

        public IQueryable<Reservation> RawQueryable() {
            throw new NotImplementedException();
        }

        public void Update(Reservation entity) {
            throw new NotImplementedException();
        }
    }
}