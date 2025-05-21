namespace PetCareBookingSystem.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        public Task<List<TEntity>> GetAll();
        public Task<TEntity> GetById(int id);
        public Task Add(TEntity entity);
        public Task Update(TEntity entity);
        public Task Delete(int id);
        public Task Save();
    }
}
