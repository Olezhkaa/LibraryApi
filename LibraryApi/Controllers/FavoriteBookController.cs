using LibraryApi.DTOs.FavoriteBooks;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/favorites")]
    public class FavoriteBookController : ControllerBase
    {
        private readonly IFavoriteBookService _favoriteBookService;

        public FavoriteBookController(IFavoriteBookService favoriteBookService)
        {
            _favoriteBookService = favoriteBookService;
        }

        // GET: api/users/1/favorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteBookDto>>> GetUserFavorites(int userId)
        {
            var favorites = await _favoriteBookService.GetUserFavoritesAsync(userId);
            return Ok(favorites);
        }

        // GET: api/users/1/favorites/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetUserFavoritesCount(int userId)
        {
            var count = await _favoriteBookService.GetUserFavoritesCountAsync(userId);
            return Ok(count);
        }

        // GET: api/users/1/favorites/5/check
        [HttpGet("{bookId}/check")]
        public async Task<ActionResult<bool>> IsBookInFavorites(int userId, int bookId)
        {
            var isInFavorites = await _favoriteBookService.IsBookInFavoritesAsync(userId, bookId);
            return Ok(isInFavorites);
        }

        // POST: api/users/1/favorites
        [HttpPost]
        public async Task<ActionResult<FavoriteBookDto>> AddToFavorites(
            int userId, [FromBody] AddToFavoritesDto addDto)
        {
            try
            {
                var favoriteBook = await _favoriteBookService.AddToFavoritesAsync(
                    userId, addDto.BookId, addDto.PriorityInList);

                return CreatedAtAction(
                    nameof(GetUserFavorites),
                    new { userId },
                    favoriteBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/users/1/favorites/5/priority
        [HttpPut("{bookId}/priority")]
        public async Task<ActionResult> UpdatePriority(int userId, int bookId, [FromBody] UpdateFavoritePriorityDto updateDto)
        {
            try
            {
                var result = await _favoriteBookService.UpdatePriorityAsync(userId, bookId, updateDto.PriorityInList);

                return result ? NoContent() : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/users/1/favorites/5
        [HttpDelete("{bookId}")]
        public async Task<ActionResult> RemoveFromFavorites(int userId, int bookId)
        {
            var result = await _favoriteBookService.RemoveFromFavoritesAsync(userId, bookId);
            return result ? NoContent() : NotFound();
        }

    }
}