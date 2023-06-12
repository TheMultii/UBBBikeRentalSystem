using Microsoft.EntityFrameworkCore;
using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class UserRepository : IRepository<User, string> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public UserRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public void Add(User user) {
            user.Id = Guid.NewGuid().ToString();
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void AddRange(IEnumerable<User> users) {
            foreach (var _user in users) {
                _user.Id = Guid.NewGuid().ToString();
                _db.Users.Add(_user);
                _db.SaveChanges();
            }
        }

        public void Delete(string id) {
            var _delete = Get(id) ?? throw new Exception("Brak takiego użytkownika w DB");
            _db.Users.Remove(_delete);
            _db.SaveChanges();
        }

        public User? Get(string id) {
            return _db.Users.SingleOrDefault(r => r.Id == id);
        }

        public List<User> GetAll() {
            return _db.Users.OrderBy(r => r.CreatedAt).ToList();
        }

        public IQueryable<User> RawQueryable() => _db.Users.AsQueryable();

        public void Update(User user) {
            var _oldVehicle = Get(user.Id) ?? throw new Exception("Brak takiego użytkownika w DB");
            _db.Entry(_oldVehicle).CurrentValues.SetValues(user);
            _db.SaveChanges();
        }
    }
}