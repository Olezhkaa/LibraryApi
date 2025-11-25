using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface ICollectionBookRepository : IRepository<CollectionBook>
    {
        Task<IEnumerable<CollectionBook>> GetByUserIdAsync(int userId);
        Task<IEnumerable<CollectionBook>> GetByCollectionIdAsync(int collectionId);
        Task<IEnumerable<CollectionBook>> GetByBookIdAsync(int bookId);
        Task<CollectionBook?> GetByUserAndBookAsync(int userId, int bookId);
        Task<CollectionBook?> GetByUserBookAndCollectionAsync(int userId, int bookId, int collectionId);
        Task<bool> ExistsAsync(int userId, int bookId, int collectionId);
        Task<bool> RemoveBookFromCollectionAsync(int userId, int bookId, int collectionId);
        Task<int> GetBooksCountInCollectionAsync(int collectionId);
        Task<IEnumerable<CollectionBook>> GetWithDetailsByCollectionAsync(int collectionId);
        Task<IEnumerable<CollectionBook>> SearchBooksInCollectionAsync(string searchTerm, int collectionId, int userId);
        Task<CollectionBook?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<CollectionBook>> GetAllWithDetailsAsync();
    }
}