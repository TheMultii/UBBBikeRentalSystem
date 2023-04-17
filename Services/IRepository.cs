namespace UBBBikeRentalSystem.Services {
    public interface IRepository<T, K> {
        List<T> GetAll();
        T? Get(K id);
        void Delete(K id);
        void Update(T entity);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        IQueryable<T> RawQueryable();
    }
}