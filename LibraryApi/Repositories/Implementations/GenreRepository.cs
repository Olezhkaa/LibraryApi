using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context) {}

        public async Task<IEnumerable<Genre>> SearchAsync(string searchTerm)
        {
            return await _dbSet.Where(g => g.Title.Contains(searchTerm) && g.IsActive == true).OrderBy(r => r.Id).ToListAsync();
        }
    }
}