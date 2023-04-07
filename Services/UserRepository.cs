using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class UserRepository : IRepository<User> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public UserRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public void Add(User user) {
            user.ID = _db.Users.Max(r => r.ID) + 1;
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void AddRange(IEnumerable<User> users) {
            int currentID = _db.Users.Max(r => r.ID) + 1;
            foreach (var _user in users) {
                _user.ID = currentID;
                _db.Users.Add(_user);
                _db.SaveChanges();
                currentID++;
            }
        }

        public void Delete(int id) {
            var _delete = Get(id) ?? throw new Exception("Brak takiego użytkownika w DB");
            _db.Users.Remove(_delete);
            _db.SaveChanges();
        }

        public User? Get(int id) {
            return _db.Users.SingleOrDefault(r => r.ID == id);
        }

        public List<User> GetAll() {
            return _db.Users.OrderBy(r => r.ID).ToList();
        }

        public IQueryable<User> RawQueryable() {
            return _db.Users.AsQueryable();
        }

        public void Update(User user) {
            var _oldVehicle = Get(user.ID) ?? throw new Exception("Brak takiego użytkownika w DB");
            _db.Entry(_oldVehicle).CurrentValues.SetValues(user);
            _db.SaveChanges();
        }
    }
}