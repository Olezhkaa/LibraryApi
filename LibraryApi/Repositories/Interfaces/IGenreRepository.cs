using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IEnumerable<Genre>> SearchAsync(string searchTerm);
    }
}