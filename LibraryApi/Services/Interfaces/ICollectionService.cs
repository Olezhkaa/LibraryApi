using LibraryApi.DTOs.Collectoins;

namespace LibraryApi.Services.Interfaces
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionDto>> GetAllAsync();
        Task<CollectionDto?> GetByIdAsync(int id);
        Task<CollectionDto> CreateAsync(CreateCollectionDto createDto);
        Task<CollectionDto?> UpdateAsync(int id, UpdateCollectionDto updateDto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<CollectionDto>> SearchAcync(string searchTerm);
    }
}