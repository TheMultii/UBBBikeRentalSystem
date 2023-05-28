using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class ReservationRepository : IRepository<Reservation, string> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public ReservationRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public void Add(Reservation reservation) {
            reservation.ID = Guid.NewGuid().ToString();
            _db.Reservations.Add(reservation);
            _db.SaveChanges();
        }

        public void AddRange(IEnumerable<Reservation> reservations) {
            foreach (var _reservation in reservations) {
                _reservation.ID = Guid.NewGuid().ToString();
                _db.Reservations.Add(_reservation);
                _db.SaveChanges();
            }
        }

        public void Delete(string id) {
            var _delete = Get(id) ?? throw new Exception("Brak takiego wypożyczenia  w DB");
            _db.Reservations.Remove(_delete);
            _db.SaveChanges();
        }

        public Reservation? Get(string id) {
            var l = _db.Reservations
                .Include(r => r.VehicleID)
                .Include(r => r.UserID)
                .Include(r => r.ReservationPoint)
                .OrderBy(r => r.ID)
                .SingleOrDefault(r => r.ID == id);
            if (l != null)
                l.ReturnPoint = _db.Reservations.Where(x => x.ID == l.ID).Select(x => x.ReturnPoint).FirstOrDefault();
            return l;
        }

        public List<Reservation> GetUsers(string userID) {
            var l = _db.Reservations
                .Include(r => r.VehicleID)
                .Include(r => r.UserID)
                .Include(r => r.ReservationPoint)
                .OrderBy(r => r.ID)
                .Where(r => r.UserID.Id == userID)
                .ToList();
            foreach (var r in l) {
                r.ReturnPoint = _db.Reservations.Where(x => x.ID == r.ID).Select(x => x.ReturnPoint).FirstOrDefault();
            }
            return l;
        }

        public List<Reservation> GetAll() {
            var l = _db.Reservations
                .Include(r => r.VehicleID)
                .Include(r => r.UserID)
                .Include(r => r.ReservationPoint)
                .OrderBy(r => r.ID)
                .ToList();
            foreach (var r in l) {
                r.ReturnPoint = _db.Reservations.Where(x => x.ID == r.ID).Select(x => x.ReturnPoint).FirstOrDefault();
            }
            return l;
        }

        public IQueryable<Reservation> RawQueryable() => _db.Reservations.AsQueryable();

        public void Update(Reservation reservation) {
            var _oldVehicle = Get(reservation.ID) ?? throw new Exception("Brak takiego wypożyczenia w DB");
            _db.Entry(_oldVehicle).CurrentValues.SetValues(reservation);
            _db.SaveChanges();
        }
    }
}