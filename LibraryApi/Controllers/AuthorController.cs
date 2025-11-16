using LibraryApi.DTOs.Authors;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(CreateAuthorDto createDto)
        {
            try
            {
                var author = await _authorService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new {id = author.Id}, author);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> Update(int id, UpdateAuthorDto updateDto)
        {
            try
            {
                var author = await _authorService.UpdateAsync(id, updateDto);
                return author == null ? NotFound() : Ok(author);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _authorService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> Search([FromQuery] string term)
        {
            var authors = await _authorService.SearchAcync(term);
            return Ok(authors);
        }

    }
}