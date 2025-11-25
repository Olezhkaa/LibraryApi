using LibraryApi.DTOs.CollectionBook;

namespace LibraryApi.Services.Interfaces
{
    public interface ICollectionBookService
    {
        Task<IEnumerable<CollectionBookDto>> GetAllAsync();
        Task<CollectionBookDto?> GetByIdAsync(int id);
        Task<CollectionBookDto> AddBookToCollectionAsync(int userId, int collectionId, int bookId);
        Task<CollectionBookDto?> MoveBookToCollectionAsync(int userId, int sourceCollectionId, int bookId, int targetCollectionId);
        Task<bool> DeleteAsync(int id);
        Task<bool> RemoveBookFromCollectionAsync(int userId, int bookId, int collectionId);
        Task<IEnumerable<CollectionBookDto>> SearchAsync(string searchTerm, int collectionId, int userId);
        Task<IEnumerable<CollectionBookDto>> GetByUserIdAsync(int userId);
        Task<IEnumerable<CollectionBookDto>> GetByBookIdAsync(int bookId);
        Task<IEnumerable<CollectionBookDto>> GetByCollectionIdAsync(int collectionId);
        Task<int> GetBooksCountInCollectionAsync(int collectionId);

    }
}