using LibraryApi.DTOs.FavoriteBooks;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteBookController : ControllerBase
    {
        private readonly IFavoriteBookService _favoriteBookService;

        public FavoriteBookController(IFavoriteBookService favoriteBookService)
        {
            _favoriteBookService = favoriteBookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteBookDto>>> GetAll()
        {
            var favoriteBooks = await _favoriteBookService.GetAllAsync();
            return Ok(favoriteBooks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteBookDto>> GetById(int id)
        {
            var favoriteBook = await _favoriteBookService.GetByIdAsync(id);
            return favoriteBook == null ? NotFound() : Ok(favoriteBook);
        }

        [HttpPost]
        public async Task<ActionResult<FavoriteBookDto>> Create(CreateFavoriteBookDto createDto)
        {
            try
            {
                var favoriteBook = await _favoriteBookService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = favoriteBook.Id }, favoriteBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FavoriteBookDto>> Update(int id, int newPriority)
        {
            try
            {
                var favoriteBook = await _favoriteBookService.ReplacePriorityInList(id, newPriority);
                return favoriteBook == null ? NotFound() : Ok(favoriteBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _favoriteBookService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FavoriteBookDto>>> Search(string term, int userId)
        {
            var favoriteBooks = await _favoriteBookService.SearchAsync(term, userId);
            return Ok(favoriteBooks);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FavoriteBookDto>>> GetByUserId(int userId)
        {
            var favoriteBooks = await _favoriteBookService.GetByUserIdAsync(userId);
            return Ok(favoriteBooks);
        }

        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<IEnumerable<FavoriteBookDto>>> GetByBookId(int bookId)
        {
            var favoriteBooks = await _favoriteBookService.GetByBookIdAsync(bookId);
            return Ok(favoriteBooks);
        }

    }
}