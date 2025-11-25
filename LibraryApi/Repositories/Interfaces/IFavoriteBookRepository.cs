using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IFavoriteBookRepository : IRepository<FavoriteBook>
    {
        Task<FavoriteBook?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<FavoriteBook>> GetAllWithDetailsAsync();
        Task<IEnumerable<FavoriteBook>> GetByUserIdAsync(int userId);
        Task<IEnumerable<FavoriteBook>> GetByBookIdAsync(int bookId);
        Task<FavoriteBook?> GetByUserAndBookAsync(int userId, int bookId);
        Task<bool> ExistsAsync(int userId, int bookId);
        Task<bool> RemoveFromFavoritesAsync(int userId, int bookId);
        Task<int> GetUserFavoritesCountAsync(int userId);
        Task<IEnumerable<FavoriteBook>> GetUserFavoritesWithDetailsAsync(int userId);
        Task<bool> UpdatePriorityAsync(int userId, int bookId, int priority);
        Task<IEnumerable<FavoriteBook>> GetUserFavoritesSortedByPriorityAsync(int userId);
    }
}