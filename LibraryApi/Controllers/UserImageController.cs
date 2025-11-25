using LibraryApi.DTOs.UserImages;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/images")]
    public class UserImageController : ControllerBase
    {
        private readonly IUserImageService _userImageService;

        public UserImageController(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserImageDto>>> GetByUserId(int userId)
        {
            var image = await _userImageService.GetByUserIdAsync(userId);
            return image == null ? NotFound() : Ok(image);
        }

        [HttpGet("{imageId}")]
        public async Task<ActionResult> GetImage(int userId, int imageId)
        {
            var image = await _userImageService.GetByIdAsync(imageId);
            if (image == null || image.UserId != userId)
                return NotFound();

            var fileData = await _userImageService.GetFileDataAsync(imageId);
            var contentType = await _userImageService.GetContentTypeAsync(imageId);

            if (fileData == null || contentType == null)
                return NotFound();

            return File(fileData, contentType, image.FileName);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<UserImageDto>> Create(int userId, [FromForm] UploadUserImageDto createDto)
        {
            try
            {
                var image = await _userImageService.CreateAsync(userId, createDto);
                return CreatedAtAction(nameof(GetImage), new { userId, imageId = image.Id }, image);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{imageId}")]
        public async Task<ActionResult> Delete(int userId, int imageId)
        {
            var result = await _userImageService.DeleteAsync(imageId);
            return result ? NoContent() : NotFound();
        }
    }
}