using System.Linq.Expressions;
using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(r => r.IsActive == true).OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Id == id && r.IsActive == true);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(r => r.Id == id  && r.IsActive == true);
            if (entity == null) return false;

            entity.IsActive = false;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).Where(r => r.IsActive == true).ToListAsync();
        }

        public async Task<T?> FirstOrDefualtAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(r => r.IsActive == true).FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(r => r.IsActive == true).AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate == null) return await _dbSet.CountAsync();
            return await _dbSet.Where(r => r.IsActive == true).Where(predicate).CountAsync();
        }

        public async Task<IEnumerable<T>> GetPageAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .Where(r => r.IsActive == true)
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync(); 
        }

    }
}