using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.Genres
{
    public class CreateGenreDto
    {
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть не менее 1 и не более 200 символов")]
        public string Title { get; set; } = string.Empty;
    }
}