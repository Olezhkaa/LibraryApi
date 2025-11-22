using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.DTOs.Books
{
    public class CreateBookDto
    {
        [Required(ErrorMessage = "Название - обязательное поле")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть не менее 1 и не более 200 символов")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Автор - обязательное поле")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Жанр - обязательное поле")]
        public int GenreId { get; set; }

        [StringLength(1000, ErrorMessage = "Описание должно быть не более 1000 символов")]
        public string? Description { get; set; }
    }
}