using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetByAuthorId(int authorId)
        {
            return await _dbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.AuthorId == authorId && b.IsActive)
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllWidthDetailsAsync()
        {
            return await _dbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.Id == id && b.IsActive)
                .OrderBy(b => b.Title)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetByGenreId(int genreId)
        {
            return await _dbSet
                .Include(b => b.Author)
                .Include(b => b.Author)
                .Where(b => b.GenreId == genreId && b.IsActive)
                .OrderBy(b => b.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => b.IsActive && (
                    b.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    b.Author!.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                    b.Author!.LastName.ToLower().Contains(searchTerm.ToLower())))
                .OrderBy(b => b.Title)
                .ToListAsync();
        }


    }
}