using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs.FavoriteBooks
{
    public class CreateFavoriteBookDto
    {
        [Required(ErrorMessage = "Поле IdUser - обязательно")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Поле IdBook - обязательно")]
        public int BookId { get; set; }
        public int PriorityInList { get; set; }
    }
}