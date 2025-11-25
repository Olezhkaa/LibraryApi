using System.Linq.Expressions;
using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories.Implementations
{
    public class UserImageRepository : Repository<UserImage>, IUserImageRepository
    {
        public UserImageRepository(AppDbContext context) : base(context) { }

        public async Task<UserImage?> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(img => img.UserId == userId && img.IsActive)
                .FirstOrDefaultAsync();
        }


    }
}