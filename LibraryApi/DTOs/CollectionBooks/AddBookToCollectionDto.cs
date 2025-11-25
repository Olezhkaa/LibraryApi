
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.CollectionBooks
{
    public class AddBookToCollectionDto
    {
        [Required(ErrorMessage = "ID книги обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID книги должен быть положительным числом")]
        public int BookId { get; set; }
    }
}