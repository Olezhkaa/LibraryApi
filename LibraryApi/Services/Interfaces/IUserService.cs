using LibraryApi.DTOs.Users;
using LibraryApi.Models;

namespace LibraryApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(CreateUserDto createDto);
        Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateDto);
        Task<bool> DeleteAsync(int id);

        Task<UserDto?> AuthAsync(string email, string password);
        Task<bool> ChangePasswordAsync(int id, string currentPassword, string newPassword);
    }
}