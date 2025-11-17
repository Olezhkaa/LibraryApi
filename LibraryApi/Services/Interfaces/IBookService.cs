using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.DTOs.Books;

namespace LibraryApi.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookDto createDto);
        Task<BookDto?> UpdateAsync(int id, UpdateBookDto updateDto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<BookDto>> SearchAcync(string searchTerm);
        Task<IEnumerable<BookDto>> GetByAuthorIdAsync(int authorId);
        Task<IEnumerable<BookDto>> GetByGenreIdAsync(int genreId);
    }
}