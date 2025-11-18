using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Collection>> SearchAsync(string searchTerm)
        {
            return await _dbSet.Where(g => g.Title.ToLower().Contains(searchTerm.ToLower()) && g.IsActive == true).OrderBy(r => r.Id).ToListAsync();
        }
    }
}