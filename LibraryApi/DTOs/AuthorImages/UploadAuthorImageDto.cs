using System.ComponentModel.DataAnnotations;
namespace LibraryApi.DTOs.AuthorImages
{
    public class UploadAuthorImageDto
    {
        [Required(ErrorMessage = "Файл обязателен")]
        public IFormFile Image { get; set; } = null!;
        public bool IsMain { get; set; } = false;
    }
}