using LibraryApi.DTOs.FavoriteBooks;

namespace LibraryApi.Services.Interfaces
{
    public interface IFavoriteBookService
    {
        Task<IEnumerable<FavoriteBookDto>> GetAllAsync();
        Task<FavoriteBookDto?> GetByIdAsync(int id);
        Task<FavoriteBookDto> CreateAsync(CreateFavoriteBookDto createDto);
        Task<FavoriteBookDto?> ReplacePriorityInList(int id, int newPriority);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<FavoriteBookDto>> SearchAsync(string searchTerm, int userId);
        Task<IEnumerable<FavoriteBookDto>> GetByUserIdAsync(int userId);
        Task<IEnumerable<FavoriteBookDto>> GetByBookIdAsync(int bookId);

    }
}