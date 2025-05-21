
using Microsoft.EntityFrameworkCore;
using PetCareBookingSystem.Models;

namespace PetCareBookingSystem.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly PetCareDBContext context;

        public GenericRepository(PetCareDBContext context)
        {
            this.context = context;
        }
        public async Task Add(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            TEntity entity = await context.Set<TEntity>().FindAsync(id);
            context.Remove(entity);
        }

        public async Task<List<TEntity>> GetAll()
        {
          return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
          context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
