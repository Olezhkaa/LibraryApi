using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    [Table("favorite_books")]
    public class FavoriteBook : BaseModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Column("book_id")]
        public int BookId { get; set; }

        [Range(1, int.MaxValue)]
        [Column("priority_in_list")]
        public int PriorityInList { get; set; }

        // Навигационные свойства
        [JsonIgnore] // Чтобы избежать циклических ссылок при сериализации
        public virtual Book? Book { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }

        protected FavoriteBook() { }

        public FavoriteBook(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}