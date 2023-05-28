﻿using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class ReservationRepository : IRepository<Reservation, string> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public ReservationRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public void Add(Reservation reservation) {
            reservation.ID = _db.Reservations.Max(r => r.ID) + 1;
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
            return _db.Reservations
                .Include(r => r.VehicleID)
                .Include(r => r.UserID)
                .Include(r => r.ReservationPoint)
                .Include(r => r.ReturnPoint)
                .SingleOrDefault(r => r.ID == id);
        }

        public List<Reservation> GetUsers(string userID) {
              return _db.Reservations
                .Include(r => r.VehicleID)
                .Include(r => r.UserID)
                .Include(r => r.ReservationPoint)
                .Include(r => r.ReturnPoint)
                .Where(r => r.UserID.Id == userID)
                .OrderBy(r => r.ID)
                .ToList();
        }

        public List<Reservation> GetAll() {
            return _db.Reservations
                .Include(r => r.VehicleID)
                .Include(r => r.UserID)
                .Include(r => r.ReservationPoint)
                .Include(r => r.ReturnPoint)
                .OrderBy(r => r.ID)
                .ToList();
        }

        public IQueryable<Reservation> RawQueryable() => _db.Reservations.AsQueryable();

        public void Update(Reservation reservation) {
            var _oldVehicle = Get(reservation.ID) ?? throw new Exception("Brak takiego wypożyczenia w DB");
            _db.Entry(_oldVehicle).CurrentValues.SetValues(reservation);
            _db.SaveChanges();
        }
    }
}