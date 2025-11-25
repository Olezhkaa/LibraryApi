using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IBookImageRepository : IRepository<BookImage>
    {
        Task<IEnumerable<BookImage>> GetByBookIdAsync(int bookId);
        Task<BookImage?> GetMainImageAsync(int bookId);
        Task<bool> SetMainImageAsync(int bookId, int imageId);
    }
}