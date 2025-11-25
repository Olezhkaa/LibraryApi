using LibraryApi.DTOs.BookImages;

namespace LibraryApi.Services.Interfaces
{
    public interface IBookImageService
    {
        Task<IEnumerable<BookImageDto>> GetByBookIdAsync(int bookId);
        Task<BookImageDto?> GetByIdAsync(int id);
        Task<BookImageDto> CreateAsync(int bookId, UploadBookImageDto createDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> SetMainAsync(int bookId, int imageId);
        Task<byte[]?> GetFileDataAsync(int imageId);
        Task<string?> GetContentTypeAsync(int imageId);
    }
}