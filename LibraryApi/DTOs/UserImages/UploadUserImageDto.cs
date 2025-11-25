using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.DTOs.UserImages
{
    public class UploadUserImageDto
    {
        [Required(ErrorMessage = "Файл обязателен")]
        public IFormFile Image { get; set; } = null!;
    }
}