using LibraryApi.DTOs.Authors;

namespace LibraryApi.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task<AuthorDto> CreateAsync(CreateAuthorDto createDto);
        Task<AuthorDto?> UpdateAsync(int id, UpdateAuthorDto updateDto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<AuthorDto>> SearchAcync(string searchTerm);
    }
}