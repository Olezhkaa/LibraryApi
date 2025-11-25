using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IAuthorImageRepository : IRepository<AuthorImage>
    {
        Task<IEnumerable<AuthorImage>> GetByAuthorIdAsync(int authorId);
        Task<AuthorImage?> GetMainImageAsync(int authorId);
        Task<bool> SetMainImageAsync(int authorId, int imageId);
    }
}