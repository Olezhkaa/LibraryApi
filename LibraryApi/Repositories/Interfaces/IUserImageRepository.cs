using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IUserImageRepository : IRepository<UserImage>
    {
        Task<UserImage?> GetByUserIdAsync(int userId);
    }
}