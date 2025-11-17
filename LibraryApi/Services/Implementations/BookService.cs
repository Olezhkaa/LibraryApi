using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.DTOs.Books;
using LibraryApi.Models;
using LibraryApi.Repositories.Interfaces;
using LibraryApi.Services.Interfaces;

namespace LibraryApi.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;


        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllWidthDetailsAsync();
            return books.Select(MapToDto).OrderBy(b => b.Title);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdWithDetailsAsync(id);
            return book == null ? null : MapToDto(book);
        }

        public async Task<BookDto> CreateAsync(CreateBookDto createDto)
        {
            //Проверка автора
            if (!await _authorRepository.ExistAsync(a => a.Id == createDto.AuthorId))
            {
                throw new ArgumentException($"Автор с ID {createDto.AuthorId} не найден");
            }
            //Проверка жанра
            if (!await _genreRepository.ExistAsync(a => a.Id == createDto.GenreId))
            {
                throw new ArgumentException($"Жанр с ID {createDto.GenreId} не найден");
            }

            var book = MapToEntity(createDto);

            var created = await _bookRepository.CreateAsync(book);
            var bookResult = await _bookRepository.GetByIdWithDetailsAsync(created.Id);

            if (bookResult == null)
            {
                throw new Exception("Книга не найдена после создания");
            }

            return MapToDto(bookResult!);
        }

        public async Task<BookDto?> UpdateAsync(int id, UpdateBookDto updateDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            //Проверка автора
            if (updateDto.AuthorId != null && !await _authorRepository.ExistAsync(a => a.Id == updateDto.AuthorId))
            {
                throw new ArgumentException($"Автор с ID {updateDto.AuthorId} не найден");
            }
            //Проверка жанра
            if (updateDto.GenreId != null && !await _genreRepository.ExistAsync(a => a.Id == updateDto.GenreId))
            {
                throw new ArgumentException($"Жанр с ID {updateDto.GenreId} не найден");
            }

            //Замена полей
            if (updateDto.Title != null || updateDto.Title != "string") book.Title = updateDto.Title!;
            if (updateDto.AuthorId != null) book.AuthorId = (int)updateDto.AuthorId;
            if (updateDto.GenreId != null) book.GenreId = (int)updateDto.GenreId;
            if (updateDto.Description != null || updateDto.Description != "string") book.Description = updateDto.Description!;

            var updated = await _bookRepository.UpdateAsync(book);
            var bookResult = await _bookRepository.GetByIdWithDetailsAsync(updated.Id);

            return MapToDto(bookResult!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookDto>> SearchAcync(string searchTerm)
        {
            var books = await _bookRepository.SearchAsync(searchTerm);
            return books.Select(MapToDto);
        }

        public async Task<IEnumerable<BookDto>> GetByAuthorIdAsync(int authorId)
        {
            var books = await _bookRepository.GetByAuthorId(authorId);
            return books.Select(MapToDto);
        }

        public async Task<IEnumerable<BookDto>> GetByGenreIdAsync(int genreId)
        {
            var books = await _bookRepository.GetByGenreId(genreId);
            return books.Select(MapToDto);
        }

        private BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title ?? string.Empty,
                AuthorFullName = book.Author?.FullName ?? "Неизвесен",
                GenreTitle = book.Genre?.Title ?? "Без жанра",
                Description = book.Description
            };
        }

        private Book MapToEntity(CreateBookDto createDto)
        {
            return new Book
            (
                title: createDto.Title,
                authorId: createDto.AuthorId,
                genreId: createDto.GenreId
            )
            {
                Description = createDto.Description
            };
        }
    }
}