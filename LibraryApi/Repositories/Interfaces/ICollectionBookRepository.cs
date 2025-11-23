using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface ICollectionBookRepository : IRepository<CollectionBook>
    {
        Task<IEnumerable<CollectionBook>> SearchBookInCollectionAsync(string searchTermBook, int collectionId, int User);
        Task<IEnumerable<CollectionBook>> GetAllWithDetailAsync();
        Task<CollectionBook?> GetByIdWithDetailAsync(int id);
        Task<IEnumerable<CollectionBook>> GetByUserIdAsync(int userId);
        Task<IEnumerable<CollectionBook>> GetByBookIdAsync(int bookId);
        Task<IEnumerable<CollectionBook>> GetByCollectionIdAsync(int collectionId);
    }
}