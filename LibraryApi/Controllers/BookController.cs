using LibraryApi.DTOs.Books;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create(CreateBookDto createDto)
        {
            try
            {
                var book = await _bookService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> Update(int id, UpdateBookDto updateDto)
        {
            try
            {
                var book = await _bookService.UpdateAsync(id, updateDto);
                return book == null ? NotFound() : Ok(book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> Search(string term)
        {
            var books = await _bookService.SearchAcync(term);
            return Ok(books);
        }

        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetByAuthorId(int authorId)
        {
            var books = await _bookService.GetByAuthorIdAsync(authorId);
            return Ok(books);
        }

        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetByGenreId(int genreId)
        {
            var books = await _bookService.GetByGenreIdAsync(genreId);
            return Ok(books);
        }

    }
}