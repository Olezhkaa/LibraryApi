using LibraryApi.DTOs.Collectoins;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;

namespace LibraryApi.Services.Implementations
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;

        public CollectionService(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<IEnumerable<CollectionDto>> GetAllAsync()
        {
            var collections = await _collectionRepository.GetAllAsync();
            return collections.Select(MapToDto);
        }

        public async Task<CollectionDto?> GetByIdAsync(int id)
        {
            var collection = await _collectionRepository.GetByIdAsync(id);
            return collection == null ? null : MapToDto(collection);
        }

        public async Task<CollectionDto> CreateAsync(CreateCollectionDto createDto)
        {
            var collection = MapToEntity(createDto);

            var create = await _collectionRepository.CreateAsync(collection);
            var collectionResult = await _collectionRepository.GetByIdAsync(create.Id);

            return MapToDto(collectionResult!);
        }

        public async Task<CollectionDto?> UpdateAsync(int id, UpdateCollectionDto updateDto)
        {
            var collection = await _collectionRepository.GetByIdAsync(id);
            if (collection == null) return null;

            //Обновление полей
            if (updateDto != null) collection.Title = updateDto.Title;

            var updated = await _collectionRepository.UpdateAsync(collection);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _collectionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CollectionDto>> SearchAcync(string searchTerm)
        {
            var collections = await _collectionRepository.SearchAsync(searchTerm);
            return collections.Select(MapToDto);
        }

        private CollectionDto MapToDto(Collection collection)
        {
            return new CollectionDto
            {
                Id = collection.Id,
                Title = collection.Title
            };
        }

        private Collection MapToEntity(CreateCollectionDto createCollectionDto)
        {
            return new Collection(title: createCollectionDto.Title);
        }
    }
}