using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.CollectionBooks
{
    public class MoveBookToCollectionDto
    {
        [Required(ErrorMessage = "ID целевой коллекции обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ID коллекции должен быть положительным числом")]
        public int TargetCollectionId { get; set; }
    }
}