namespace UBBBikeRentalSystem.Services {
    public interface IRepository<T> {
        List<T> GetAll();
        T? Get(int id);
        void Delete(int id);
        void Update(T entity);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        IQueryable<T> RawQueryable();
    }
}