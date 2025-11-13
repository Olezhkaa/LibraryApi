using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    [Table("books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        [Column("author_id")]
        public int AuthorId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Column("genre_id")]
        public int GenreId { get; set; }

        [Column("description")]
        [StringLength(1000)]
        public string? Description { get; set; }

        [JsonIgnore]
        public virtual Author? Author { get; set; }

        [JsonIgnore]
        public virtual Genre? Genre { get; set; }

        protected Book() { }

        public Book(string title, int authorId, int genreId)
        {
            Title = title;
            AuthorId = authorId;
            GenreId = genreId;
        }
    }
}