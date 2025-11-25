using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.BookImages
{
    public class UploadBookImageDto
    {
        [Required(ErrorMessage = "Файл обязателен")]
        public IFormFile Image { get; set; } = null!;
        public bool IsMain { get; set; } = false;
    }
}