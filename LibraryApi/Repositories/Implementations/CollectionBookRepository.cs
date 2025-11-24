using System.Linq.Expressions;
using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class CollectionBookRepository : Repository<CollectionBook>, ICollectionBookRepository
    {
        public CollectionBookRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<CollectionBook>> SearchBookInCollectionAsync(string searchTermBook, int collectionId, int userId)
        {
            return await _dbSet
                .Include(cb => cb.User)
                .Include(cb => cb.Book)
                .Include(cb => cb.Collection)
                .Where(cb => cb.IsActive && cb.Book!.Title.ToLower().Contains(searchTermBook.ToLower()) && cb.CollectionId == collectionId && cb.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CollectionBook>> GetAllWithDetailAsync()
        {
            return await _dbSet
                .Include(cb => cb.User)
                .Include(cb => cb.Book)
                .Include(cb => cb.Collection)
                .Where(cb => cb.IsActive)
                .ToListAsync();
        }

        public async Task<CollectionBook?> GetByIdWithDetailAsync(int id)
        {
            return await _dbSet
                .Include(cb => cb.User)
                .Include(cb => cb.Book)
                .Include(cb => cb.Collection)
                .Where(cb => cb.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CollectionBook>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(cb => cb.User)
                .Include(cb => cb.Book)
                .Include(cb => cb.Collection)
                .Where(cb => cb.IsActive && cb.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CollectionBook>> GetByBookIdAsync(int bookId)
        {
            return await _dbSet
                .Include(cb => cb.User)
                .Include(cb => cb.Book)
                .Include(cb => cb.Collection)
                .Where(cb => cb.IsActive && cb.BookId == bookId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CollectionBook>> GetByCollectionIdAsync(int collectionId)
        {
            return await _dbSet
                 .Include(cb => cb.User)
                 .Include(cb => cb.Book)
                 .Include(cb => cb.Collection)
                 .Where(cb => cb.IsActive && cb.CollectionId == collectionId)
                 .ToListAsync();
        }
    }
}