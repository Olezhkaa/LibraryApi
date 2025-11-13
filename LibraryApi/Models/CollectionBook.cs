using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    [Table("collection_book")]
    public class CollectionBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Column("book_id")]
        public int BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Column("collection_id")]
        public int CollectionId { get; set; }

        // Навигационные свойства
        [JsonIgnore] // Чтобы избежать циклических ссылок при сериализации
        public virtual Book? Book { get; set; }

        // [JsonIgnore]
        // public virtual User? User { get; set; }

        [JsonIgnore]
        public virtual Collection? Collection { get; set; }

        protected CollectionBook() { }

        public CollectionBook(int userId, int bookId)
        {
            UserId = userId;
            BookId = bookId;
        }
    }
}