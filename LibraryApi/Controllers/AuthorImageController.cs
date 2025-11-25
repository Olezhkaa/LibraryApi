using LibraryApi.DTOs.AuthorImages;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/images")]
    public class AuthorImageController : ControllerBase
    {
        private readonly IAuthorImageService _authorImageService;

        public AuthorImageController(IAuthorImageService authorImageService)
        {
            _authorImageService = authorImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorImageDto>>> GetByAuthorId(int authorId)
        {
            var images = await _authorImageService.GetByAuthorIdAsync(authorId);
            return Ok(images);
        }

        [HttpGet("{imageId}")]
        public async Task<ActionResult> GetImage(int authorId, int imageId)
        {
            var image = await _authorImageService.GetByIdAsync(imageId);
            if (image == null || image.AuthorId != authorId)
                return NotFound();

            var fileData = await _authorImageService.GetFileDataAsync(imageId);
            var contentType = await _authorImageService.GetContentTypeAsync(imageId);

            if (fileData == null || contentType == null)
                return NotFound();

            return File(fileData, contentType, image.FileName);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<AuthorImageDto>> Create(int authorId, [FromForm] UploadAuthorImageDto createDto)
        {
            try
            {
                var image = await _authorImageService.CreateAsync(authorId, createDto);
                return CreatedAtAction(nameof(GetImage), new { authorId, imageId = image.Id }, image);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{imageId}")]
        public async Task<ActionResult> Delete(int authorId, int imageId)
        {
            var result = await _authorImageService.DeleteAsync(imageId);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("{imageId}/set-main")]
        public async Task<ActionResult> SetMain(int authorId, int imageId)
        {
            var result = await _authorImageService.SetMainAsync(authorId, imageId);
            return result ? NoContent() : NotFound();
        }
    }
}