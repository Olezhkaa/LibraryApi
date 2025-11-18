using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Author>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Where(a => a.IsActive == true &&
                (a.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                a.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                (a.MiddleName != null && a.MiddleName.ToLower().Contains(searchTerm.ToLower()))))
                .OrderBy(a => a.LastName)
                .ToListAsync();
        }

    }
}