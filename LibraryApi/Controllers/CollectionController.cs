using LibraryApi.DTOs.Collectoins;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAll()
        {
            var collections = await _collectionService.GetAllAsync();
            return Ok(collections);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionDto>> GetById(int id)
        {
            var collection = await _collectionService.GetByIdAsync(id);
            return collection == null ? NotFound() : Ok(collection);
        }

        [HttpPost]
        public async Task<ActionResult<CollectionDto>> Create(CreateCollectionDto createDto)
        {
            try
            {
                var collection = await _collectionService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = collection.Id }, collection);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CollectionDto>> Update(int id, UpdateCollectionDto updateDto)
        {
            try
            {
                var collection = await _collectionService.UpdateAsync(id, updateDto);
                return collection == null ? NotFound() : Ok(collection);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _collectionService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> Search([FromQuery] string term)
        {
            var collections = await _collectionService.SearchAcync(term);
            return Ok(collections);
        }
    }
}