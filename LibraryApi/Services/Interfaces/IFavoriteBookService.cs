using LibraryApi.DTOs.FavoriteBooks;

namespace LibraryApi.Services.Interfaces
{
    public interface IFavoriteBookService
    {
        Task<IEnumerable<FavoriteBookDto>> GetAllAsync();
        Task<FavoriteBookDto?> GetByIdAsync(int id);
        Task<FavoriteBookDto> AddToFavoritesAsync(int userId, int bookId, int priority = 1);
        Task<bool> RemoveFromFavoritesAsync(int userId, int bookId);
        Task<bool> UpdatePriorityAsync(int userId, int bookId, int priority);
        Task<bool> IsBookInFavoritesAsync(int userId, int bookId);
        Task<IEnumerable<FavoriteBookDto>> GetUserFavoritesAsync(int userId);
        Task<int> GetUserFavoritesCountAsync(int userId);
    }
}