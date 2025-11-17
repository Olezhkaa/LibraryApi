using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> SearchAsync(string searchTerm);
        Task<IEnumerable<Book>> GetAllWidthDetailsAsync();
        Task<Book?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Book>> GetByAuthorId(int authorId);
        Task<IEnumerable<Book>> GetByGenreId(int genreId);
    }
}