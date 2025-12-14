using LibraryApi.DTOs.Genres;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genre = await _genreService.GetByIdAsync(id);
            return genre == null ? NotFound() : Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create(CreateGenreDto createDto)
        {
            try
            {
                var genre = await _genreService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new {id = genre.Id}, genre);
            } 
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreDto>> Update(int id, UpdateGenreDto updateDto)
        {
            try
            {
                var genre = await _genreService.UpdateAsync(id, updateDto);
                return genre == null ? NotFound() : Ok(genre);
            } 
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _genreService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<GenreDto>>> Search([FromQuery] string term)
        {
            var genres = await _genreService.SearchAcync(term);
            return Ok(genres);
        }

        
    }
}