using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Task<IEnumerable<Collection>> SearchAsync(string searchTerm);
    }
}