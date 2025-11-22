using LibraryApi.Models;

namespace LibraryApi.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);

        string HashPassword(string password);
        bool VerifyPassword(string password, string HashPassword);

    }
}