using LibraryApi.DTOs.Authors;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;

namespace LibraryApi.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors.Select(MapToDto).OrderBy(a => a.LastName);
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author == null ? null : MapToDto(author);
        }

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto createDto)
        {
            var author = new Author
            (
                lastName: createDto.LastName,
                firstName: createDto.FirstName
            )
            {
                MiddleName = createDto.MiddleName,
                DateOfBirh = createDto.DateOfBirh,
                DateOfDeath = createDto.DateOfDeath,
                Country = createDto.Country
            };

            var create = await _authorRepository.CreateAsync(author);
            var authorResult = await _authorRepository.GetByIdAsync(create.Id);

            return MapToDto(authorResult!);
        }
        public async Task<AuthorDto?> UpdateAsync(int id, UpdateAuthorDto updateDto)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return null;

            if (updateDto.LastName != null || updateDto.LastName != "string") author.LastName = updateDto.LastName!;
            if (updateDto.FirstName != null || updateDto.FirstName != "string") author.FirstName = updateDto.FirstName!;
            if (updateDto.MiddleName != null || updateDto.MiddleName != "string") author.MiddleName = updateDto.MiddleName!;
            if (updateDto.DateOfBirh != null) author.DateOfBirh = updateDto.DateOfBirh!;
            if (updateDto.DateOfDeath != null) author.DateOfDeath = updateDto.DateOfDeath!;
            if (updateDto.Country != null || updateDto.LastName != "string") author.Country = updateDto.Country!;

            var update = await _authorRepository.UpdateAsync(author);
            return MapToDto(update);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _authorRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<AuthorDto>> SearchAcync(string searchTerm)
        {
            var authors = await _authorRepository.SearchAsync(searchTerm);
            return authors.Select(MapToDto).OrderBy(a => a.LastName);
        }

        private AuthorDto MapToDto(Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                LastName = author.LastName,
                FirstName = author.FirstName,
                MiddleName = author.MiddleName,
                DateOfBirh = author.DateOfBirh,
                DateOfDeath = author.DateOfDeath,
                Country = author.Country
            };
        }
    }
}