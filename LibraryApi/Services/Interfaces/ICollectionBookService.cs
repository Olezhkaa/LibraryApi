using LibraryApi.DTOs.CollectionBook;

namespace LibraryApi.Services.Interfaces
{
    public interface ICollectionBookService
    {
        Task<IEnumerable<CollectionBookDto>> GetAllAsync();
        Task<CollectionBookDto?> GetByIdAsync(int id);
        Task<CollectionBookDto> CreateAsync(CreateCollectionBookDto createDto);
        Task<CollectionBookDto?> ReplaceBookCollectionDto(int id, CreateCollectionBookDto createDto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<CollectionBookDto>> SearchAsync(string searchTerm, int collectionId, int userId);
        Task<IEnumerable<CollectionBookDto>> GetByUserIdAsync(int userId);
        Task<IEnumerable<CollectionBookDto>> GetByBookIdAsync(int bookId);
        Task<IEnumerable<CollectionBookDto>> GetByCollectionIdAsync(int collectionId);

    }
}