using System.Linq.Expressions;
using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class BookImageRepository : Repository<BookImage>, IBookImageRepository
    {
        public BookImageRepository(AppDbContext context) : base(context) { }


        public async Task<IEnumerable<BookImage>> GetByBookIdAsync(int bookId)
        {
            return await _dbSet
                .Where(img => img.BookId == bookId && img.IsActive)
                .OrderByDescending(img => img.IsMain)
                .ThenByDescending(img => img.CreatedAt)
                .ToListAsync();
        }

        public async Task<BookImage?> GetMainImageAsync(int bookId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(img => img.BookId == bookId && img.IsMain && img.IsActive);
        }

        public async Task<bool> SetMainImageAsync(int bookId, int imageId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Снимаем флаг main со всех изображений автора
                var currentMainImages = await _dbSet
                    .Where(img => img.BookId == bookId && img.IsMain && img.IsActive)
                    .ToListAsync();

                foreach (var img in currentMainImages)
                {
                    img.IsMain = false;
                }

                // Устанавливаем флаг main для указанного изображения
                var targetImage = await _dbSet
                    .FirstOrDefaultAsync(img => img.Id == imageId && img.BookId == bookId && img.IsActive);

                if (targetImage == null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                targetImage.IsMain = true;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}