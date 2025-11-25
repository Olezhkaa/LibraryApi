using LibraryApi.DTOs.BookImages;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/books/{bookId}/images")]
    public class BookImageController : ControllerBase
    {
        private readonly IBookImageService _bookImageService;

        public BookImageController(IBookImageService bookImageService)
        {
            _bookImageService = bookImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookImageDto>>> GetByBookId(int bookId)
        {
            var images = await _bookImageService.GetByBookIdAsync(bookId);
            return Ok(images);
        }

        [HttpGet("{imageId}")]
        public async Task<ActionResult> GetImage(int bookId, int imageId)
        {
            var image = await _bookImageService.GetByIdAsync(imageId);
            if (image == null || image.BookId != bookId)
                return NotFound();

            var fileData = await _bookImageService.GetFileDataAsync(imageId);
            var contentType = await _bookImageService.GetContentTypeAsync(imageId);

            if (fileData == null || contentType == null)
                return NotFound();

            return File(fileData, contentType, image.FileName);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<BookImageDto>> Create(int bookId, [FromForm] UploadBookImageDto createDto)
        {
            try
            {
                var image = await _bookImageService.CreateAsync(bookId, createDto);
                return CreatedAtAction(nameof(GetImage), new { bookId, imageId = image.Id }, image);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{imageId}")]
        public async Task<ActionResult> Delete(int bookId, int imageId)
        {
            var result = await _bookImageService.DeleteAsync(imageId);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("{imageId}/set-main")]
        public async Task<ActionResult> SetMain(int bookId, int imageId)
        {
            var result = await _bookImageService.SetMainAsync(bookId, imageId);
            return result ? NoContent() : NotFound();
        }
    }
}