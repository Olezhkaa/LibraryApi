using LibraryApi.Data;
using LibraryApi.DTOs.Genres;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return genres.Select(MapToDto);
        }

        public async Task<GenreDto?> GetByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return genre == null ? null : MapToDto(genre);
        }

        public async Task<GenreDto> CreateAsync(CreateGenreDto createDto)
        {
            var genre = MapToEntity(createDto);

            var create = await _genreRepository.CreateAsync(genre);
            var genreResult = await _genreRepository.GetByIdAsync(create.Id);

            return MapToDto(genreResult!);
        }

        public async Task<GenreDto?> UpdateAsync(int id, UpdateGenreDto updateDto)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if(genre == null) return null;

            //Обновление полей
            if (updateDto != null) genre.Title = updateDto.Title;

            var updated = await _genreRepository.UpdateAsync(genre);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _genreRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GenreDto>> SearchAcync(string searchTerm)
        {
            var genres = await _genreRepository.SearchAsync(searchTerm);
            return genres.Select(MapToDto);
        }

        private GenreDto MapToDto(Genre genre)
        {
            return new GenreDto
            {
              Id = genre.Id,
              Title = genre.Title  
            };
        }

        private Genre MapToEntity(CreateGenreDto createGenreDto)
        {
            return new Genre(title: createGenreDto.Title);
        }
    }
}