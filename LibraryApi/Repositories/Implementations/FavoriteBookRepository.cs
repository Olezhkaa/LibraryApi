using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class FavoriteBookRepository : Repository<FavoriteBook>, IFavoriteBookRepository
    {
        public FavoriteBookRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<FavoriteBook>> SearchBookAsync(string searchTermBook, int userId)
        {
            return await _dbSet
                .Include(fb => fb.User)
                .Include(fb => fb.Book)
                .Where(fb => fb.IsActive && fb.Book!.Title.ToLower().Contains(searchTermBook.ToLower()) && fb.UserId == userId)
                .OrderBy(fb => fb.PriorityInList)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteBook>> GetAllWithDetailAsync()
        {
            return await _dbSet
                .Include(fb => fb.User)
                .Include(fb => fb.Book)
                .Where(fb => fb.IsActive)
                .ToListAsync();
        }

        public async Task<FavoriteBook?> GetByIdWithDetailAsync(int id)
        {
            return await _dbSet
                .Include(fb => fb.User)
                .Include(fb => fb.Book)
                .Where(fb => fb.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FavoriteBook>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(fb => fb.User)
                .Include(fb => fb.Book)
                .Where(fb => fb.IsActive && fb.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteBook>> GetByBookIdAsync(int bookId)
        {
            return await _dbSet
                .Include(fb => fb.User)
                .Include(fb => fb.Book)
                .Where(fb => fb.IsActive && fb.BookId == bookId)
                .ToListAsync();
        }
    }
}