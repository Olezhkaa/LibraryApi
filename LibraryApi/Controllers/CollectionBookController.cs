using LibraryApi.DTOs.CollectionBook;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionBookController : ControllerBase
    {
        private readonly ICollectionBookService _collectionBookService;

        public CollectionBookController(ICollectionBookService collectionBookService)
        {
            _collectionBookService = collectionBookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionBookDto>>> GetAll()
        {
            var collectionBooks = await _collectionBookService.GetAllAsync();
            return Ok(collectionBooks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionBookDto>> GetById(int id)
        {
            var collectionBook = await _collectionBookService.GetByIdAsync(id);
            return collectionBook == null ? NotFound() : Ok(collectionBook);
        }

        [HttpPost]
        public async Task<ActionResult<CollectionBookDto>> Create(CreateCollectionBookDto createDto)
        {
            try
            {
                var collectionBook = await _collectionBookService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = collectionBook.Id }, collectionBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CollectionBookDto>> Update(int id, CreateCollectionBookDto updateDto)
        {
            try
            {
                var collectionBook = await _collectionBookService.ReplaceBookCollectionDto(id, updateDto);
                return collectionBook == null ? NotFound() : Ok(collectionBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _collectionBookService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CollectionBookDto>>> Search(string term, int collectionId, int userId)
        {
            var collectionBooks = await _collectionBookService.SearchAsync(term, collectionId, userId);
            return Ok(collectionBooks);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CollectionBookDto>>> GetByUserId(int userId)
        {
            var collectionBooks = await _collectionBookService.GetByUserIdAsync(userId);
            return Ok(collectionBooks);
        }

        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<IEnumerable<CollectionBookDto>>> GetByBookId(int bookId)
        {
            var collectionBooks = await _collectionBookService.GetByBookIdAsync(bookId);
            return Ok(collectionBooks);
        }

        [HttpGet("collection/{collectionId}")]
        public async Task<ActionResult<IEnumerable<CollectionBookDto>>> GetByCollectionId(int collectionId)
        {
            var collectionBooks = await _collectionBookService.GetByCollectionIdAsync(collectionId);
            return Ok(collectionBooks);
        }
    }
}