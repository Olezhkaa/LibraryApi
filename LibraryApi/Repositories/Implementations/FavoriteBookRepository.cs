using System.Data;
using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryApi.Repositories.Implementations
{
    public class FavoriteBookRepository : Repository<FavoriteBook>, IFavoriteBookRepository
    {
        public FavoriteBookRepository(AppDbContext context) : base(context) { }

        public async Task<FavoriteBook?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.Author)
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.BookImages.Where(bi => bi.IsMain))
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.Genre)
                .Include(fb => fb.User)
                .FirstOrDefaultAsync(fb => fb.Id == id && fb.IsActive);
        }

        public async Task<IEnumerable<FavoriteBook>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.Author)
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.BookImages.Where(bi => bi.IsMain))
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.Genre)
                .Include(fb => fb.User)
                .Where(fb => fb.IsActive)
                .OrderBy(fb => fb.UserId)
                .ThenBy(fb => fb.PriorityInList)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteBook>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(fb => fb.UserId == userId && fb.IsActive)
                .OrderBy(fb => fb.PriorityInList)
                .ThenByDescending(fb => fb.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteBook>> GetByBookIdAsync(int bookId)
        {
            return await _dbSet
                .Where(fb => fb.BookId == bookId && fb.IsActive)
                .ToListAsync();
        }

        public async Task<FavoriteBook?> GetByUserAndBookAsync(int userId, int bookId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(fb => fb.UserId == userId && fb.BookId == bookId && fb.IsActive);
        }

        public async Task<bool> ExistsAsync(int userId, int bookId)
        {
            return await _dbSet
                .AnyAsync(fb => fb.UserId == userId && fb.BookId == bookId && fb.IsActive);
        }

        public async Task<bool> RemoveFromFavoritesAsync(int userId, int bookId)
        {
            var favoriteBook = await _dbSet
                .FirstOrDefaultAsync(fb => fb.UserId == userId && fb.BookId == bookId && fb.IsActive);

            if (favoriteBook == null) return false;

            favoriteBook.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetUserFavoritesCountAsync(int userId)
        {
            return await _dbSet
                .CountAsync(fb => fb.UserId == userId && fb.IsActive);
        }

        public async Task<IEnumerable<FavoriteBook>> GetUserFavoritesWithDetailsAsync(int userId)
        {
            return await _dbSet
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.Author)
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.BookImages.Where(bi => bi.IsMain))
                .Include(fb => fb.Book)
                    .ThenInclude(b => b!.Genre)
                .Where(fb => fb.UserId == userId && fb.IsActive)
                .OrderBy(fb => fb.PriorityInList)
                .ThenByDescending(fb => fb.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdatePriorityAsync(int userId, int bookId, int priority)
        {
            var favoriteBook = await _dbSet
                .FirstOrDefaultAsync(fb => fb.UserId == userId && fb.BookId == bookId && fb.IsActive);

            if (favoriteBook == null) return false;

            favoriteBook.PriorityInList = priority;
            favoriteBook.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FavoriteBook>> GetUserFavoritesSortedByPriorityAsync(int userId)
        {
            return await _dbSet
                .Include(fb => fb.Book)
                .Where(fb => fb.UserId == userId && fb.IsActive)
                .OrderBy(fb => fb.PriorityInList)
                .ToListAsync();
        }
    }
}