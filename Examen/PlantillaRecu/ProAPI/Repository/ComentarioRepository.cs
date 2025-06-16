using Microsoft.EntityFrameworkCore;
using RestAPI.Data;
using RestAPI.Models.Entity;
using RestAPI.Repository.IRepository;

namespace RestAPI.Repository
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ComentarioEntity> _dbSet;

        public ComentarioRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<ComentarioEntity>();
        }

        public async Task<ICollection<ComentarioEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<ComentarioEntity> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CreateAsync(ComentarioEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(ComentarioEntity entity)
        {
            _dbSet.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void ClearCache() { }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ComentarioEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
