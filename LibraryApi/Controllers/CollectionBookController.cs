using LibraryApi.DTOs.CollectionBook;
using LibraryApi.DTOs.CollectionBooks;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/collections")]
    public class CollectionBookController : ControllerBase
    {
        private readonly ICollectionBookService _collectionBookService;

        public CollectionBookController(ICollectionBookService collectionBookService)
        {
            _collectionBookService = collectionBookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionBookDto>>> GetCollectionBooks(int userId)
        {
            var collections = await _collectionBookService.GetByUserIdAsync(userId);
            return Ok(collections);
        }

        [HttpGet("{collectionId}/books")]
        public async Task<ActionResult<IEnumerable<CollectionBookDto>>> GetBooksInCollection(int userId, int collectionId)
        {
            var books = await _collectionBookService.GetByCollectionIdAsync(collectionId);
            return Ok(books);
        }

        [HttpGet("{collectionId}/books/{bookId}")]
        public async Task<ActionResult<CollectionBookDto>> GetBookInCollection(int userId, int collectionId, int bookId)
        {
            var collections = await _collectionBookService.GetByCollectionIdAsync(collectionId);
            var collectionBook = collections.FirstOrDefault(cb => cb.BookId == bookId);

            if (collectionBook == null)
                return NotFound();

            if (collectionBook.UserId != userId)
                return Forbid();

            return Ok(collectionBook);
        }

        [HttpPost("{collectionId}/books")]
        public async Task<ActionResult<CollectionBookDto>> AddBookToCollection(
            int userId, int collectionId, [FromBody] AddBookToCollectionDto addDto)
        {
            try
            {
                var collectionBook = await _collectionBookService.AddBookToCollectionAsync(
                    userId, collectionId, addDto.BookId);

                return CreatedAtAction(
                    nameof(GetBookInCollection),
                    new { userId, collectionId, bookId = collectionBook.BookId },
                    collectionBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{collectionId}/books/{bookId}/move")]
        public async Task<ActionResult<CollectionBookDto>> MoveBookToCollection(
            int userId, int collectionId, int bookId, [FromBody] MoveBookToCollectionDto moveDto)
        {
            try
            {
                var result = await _collectionBookService.MoveBookToCollectionAsync(
                    userId, collectionId, bookId, moveDto.TargetCollectionId);

                return result == null ? NotFound("Книга не найдена в указанной коллекции") : Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{collectionId}/books/{bookId}")]
        public async Task<ActionResult> RemoveBookFromCollection(int userId, int collectionId, int bookId)
        {
            var result = await _collectionBookService.RemoveBookFromCollectionAsync(userId, bookId, collectionId);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CollectionBookDto>>> SearchBooks(
            int userId, [FromQuery] string term, [FromQuery] int collectionId)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Поисковый запрос не может быть пустым");

            var books = await _collectionBookService.SearchAsync(term, collectionId, userId);
            return Ok(books);
        }

        [HttpGet("{collectionId}/books/count")]
        public async Task<ActionResult<int>> GetBooksCount(int userId, int collectionId)
        {
            var collections = await _collectionBookService.GetByCollectionIdAsync(collectionId);

            var count = await _collectionBookService.GetBooksCountInCollectionAsync(collectionId);
            return Ok(count);
        }
    }
}