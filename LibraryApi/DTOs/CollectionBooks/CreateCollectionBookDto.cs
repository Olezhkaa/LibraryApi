using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.CollectionBook
{
    public class CreateCollectionBookDto
    {
        [Required(ErrorMessage = "Поле IdUser - обязательно")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Поле IdBook - обязательно")]
        public int BookId { get; set; }
        [Required(ErrorMessage = "Поле IdCollection - обязательно")]
        public int CollectionId { get; set; }
    }
}