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

        public async Task<IEnumerable<CollectionBook>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.Author)
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.BookImages)
                .Include(uc => uc.Collection)
                .Include(uc => uc.User)
                .Where(uc => uc.UserId == userId && uc.IsActive)
                .OrderByDescending(uc => uc.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<CollectionBook>> GetByCollectionIdAsync(int collectionId)
        {
            return await _dbSet
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.Author)
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.BookImages)
                .Include(uc => uc.User)
                .Include(us => us.Collection)
                .Where(uc => uc.CollectionId == collectionId && uc.IsActive)
                .OrderByDescending(uc => uc.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<CollectionBook>> GetByBookIdAsync(int bookId)
        {
            return await _dbSet
                .Include(uc => uc.Collection)
                .Include(uc => uc.User)
                .Include(us => us.Book)
                .Where(uc => uc.BookId == bookId && uc.IsActive)
                .ToListAsync();
        }

        public async Task<CollectionBook?> GetByUserAndBookAsync(int userId, int bookId)
        {
            return await _dbSet
                .Include(uc => uc.Collection)
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.BookId == bookId && uc.IsActive);
        }

        public async Task<CollectionBook?> GetByUserBookAndCollectionAsync(int userId, int bookId, int collectionId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(uc => uc.UserId == userId &&
                                          uc.BookId == bookId &&
                                          uc.CollectionId == collectionId &&
                                          uc.IsActive);
        }

        public async Task<bool> ExistsAsync(int userId, int bookId, int collectionId)
        {
            return await _dbSet
                .AnyAsync(uc => uc.UserId == userId &&
                               uc.BookId == bookId &&
                               uc.CollectionId == collectionId &&
                               uc.IsActive);
        }

        public async Task<bool> RemoveBookFromCollectionAsync(int userId, int bookId, int collectionId)
        {
            var collectionBook = await _dbSet
                .FirstOrDefaultAsync(uc => uc.UserId == userId &&
                                          uc.BookId == bookId &&
                                          uc.CollectionId == collectionId &&
                                          uc.IsActive);

            if (collectionBook == null) return false;

            collectionBook.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetBooksCountInCollectionAsync(int collectionId)
        {
            return await _dbSet
                .CountAsync(uc => uc.CollectionId == collectionId && uc.IsActive);
        }

        public async Task<IEnumerable<CollectionBook>> GetWithDetailsByCollectionAsync(int collectionId)
        {
            return await _dbSet
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.Author)
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.Genre)
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.BookImages)
                .Include(uc => uc.User)
                .Where(uc => uc.CollectionId == collectionId && uc.IsActive)
                .OrderByDescending(uc => uc.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<CollectionBook>> SearchBooksInCollectionAsync(string searchTerm, int collectionId, int userId)
        {
            return await _dbSet
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.Author)
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.BookImages)
                .Include(uc => uc.User)
                .Where(uc => uc.CollectionId == collectionId &&
                            uc.UserId == userId &&
                            uc.IsActive &&
                            (uc.Book!.Title.Contains(searchTerm) ||
                             uc.Book.Author!.FirstName.Contains(searchTerm) ||
                             uc.Book.Author.LastName.Contains(searchTerm)))
                .OrderByDescending(uc => uc.CreatedAt)
                .ToListAsync();
        }

        public async Task<CollectionBook?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(uc => uc.User)
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.Author)
                .Include(uc => uc.Book)
                    .ThenInclude(b => b!.BookImages)
                .Include(uc => uc.Collection)
                .FirstOrDefaultAsync(uc => uc.Id == id && uc.IsActive);
        }

        public async Task<IEnumerable<CollectionBook>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(uc => uc.User)
                .Include(uc => uc.Book)
                .Include(uc => uc.Collection)
                .Where(uc => uc.IsActive)
                .ToListAsync();
        }
    }
}