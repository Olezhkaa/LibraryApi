using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.DTOs.Books
{
    public class UpdateBookDto
    {
        [StringLength(200, ErrorMessage = "Название должно быть не более 200 символов")]
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public int? GenreId { get; set; }

        [StringLength(1000, ErrorMessage = "Описание должно быть не более 1000 символов")]
        public string? Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}