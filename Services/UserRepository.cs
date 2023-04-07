using UBBBikeRentalSystem.Database;
using UBBBikeRentalSystem.Models;

namespace UBBBikeRentalSystem.Services {
    public class UserRepository : IRepository<User> {
        private readonly UBBBikeRentalSystemDatabase _db;

        public UserRepository(UBBBikeRentalSystemDatabase db) {
            _db = db;
        }

        public void Add(User entity) {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<User> entities) {
            throw new NotImplementedException();
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public User? Get(int id) {
            throw new NotImplementedException();
        }

        public List<User> GetAll() {
            throw new NotImplementedException();
        }

        public IQueryable<User> RawQueryable() {
            throw new NotImplementedException();
        }

        public void Update(User entity) {
            throw new NotImplementedException();
        }
    }
}