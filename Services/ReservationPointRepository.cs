using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class ReservationPointRepository: IRepository<ReservationPoint> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public ReservationPointRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public void Add(ReservationPoint entity) {
            entity.ID = _db.ReservationPoints.Max(x => x.ID) + 1;
            _db.ReservationPoints.Add(entity);
            _db.SaveChanges();
        }

        public void AddRange(IEnumerable<ReservationPoint> entities) {
            int currentID = _db.ReservationPoints.Max(x => x.ID) + 1;
            foreach (ReservationPoint entity in entities) {
                entity.ID = currentID;
                _db.ReservationPoints.Add(entity);
                currentID++;
            }
            _db.SaveChanges();
        }

        public void Delete(int id) {
            ReservationPoint? entity = _db.ReservationPoints.SingleOrDefault(x => x.ID == id) ?? throw new Exception("Brak takiego punktu w DB");
            _db.ReservationPoints.Remove(entity);
            _db.SaveChanges();
        }

        public ReservationPoint? Get(int id) {
            return _db.ReservationPoints.SingleOrDefault(x => x.ID == id);
        }

        public List<ReservationPoint> GetAll() {
            return _db.ReservationPoints.OrderBy(r => r.ID).ToList();
        }

        public IQueryable<ReservationPoint> RawQueryable() {
            return _db.ReservationPoints.AsQueryable();
        }

        public void Update(ReservationPoint entity) {
            var _oldReservationPoint = _db.ReservationPoints.SingleOrDefault(x => x.ID == entity.ID) ?? throw new Exception("Brak takiego punktu w DB");
            _db.Entry(_oldReservationPoint).CurrentValues.SetValues(entity);
            _db.SaveChanges();
        }
    }
}
