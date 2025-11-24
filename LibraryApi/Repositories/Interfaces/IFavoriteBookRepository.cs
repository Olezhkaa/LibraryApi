using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IFavoriteBookRepository : IRepository<FavoriteBook>
    {
        Task<IEnumerable<FavoriteBook>> SearchBookAsync(string searchTermBook, int User);
        Task<IEnumerable<FavoriteBook>> GetAllWithDetailAsync();
        Task<FavoriteBook?> GetByIdWithDetailAsync(int id);
        Task<IEnumerable<FavoriteBook>> GetByUserIdAsync(int userId);
        Task<IEnumerable<FavoriteBook>> GetByBookIdAsync(int bookId);
    }
}