using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Models
{
    [Table("genres")]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        protected Genre() { }

        public Genre(string title)
        {
            Title = title;
        }
    }
}