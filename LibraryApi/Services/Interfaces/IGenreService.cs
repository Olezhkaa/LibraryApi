using LibraryApi.DTOs.Genres;

namespace LibraryApi.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();
        Task<GenreDto?> GetByIdAsync(int id);
        Task<GenreDto> CreateAsync(CreateGenreDto createDto);
        Task<GenreDto?> UpdateAsync(int id, UpdateGenreDto updateDto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<GenreDto>> SearchAcync(string searchTerm);
    }
}