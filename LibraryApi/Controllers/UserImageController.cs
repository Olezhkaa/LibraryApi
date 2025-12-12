using LibraryApi.DTOs.UserImages;
using LibraryApi.Models;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/image")]
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
            if(image == null) return NotFound();

            var fileData = await _userImageService.GetFileDataAsync(image.Id);
            var contentType = await _userImageService.GetContentTypeAsync(image.Id);

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
                return CreatedAtAction(nameof(GetByUserId), new { userId }, image);
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