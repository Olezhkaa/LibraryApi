using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Models
{
    [Table("authors")]
    public class Author : BaseModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        [Column("middle_name")]
        public string? MiddleName { get; set; } = string.Empty;

        [Column("date_of_birth")]
        public DateOnly? DateOfBirh { get; set; }

        [Column("date_of_death")]
        public DateOnly? DateOfDeath { get; set; }

        [StringLength(100)]
        [Column("country")]
        public string? Country { get; set; } = string.Empty;

        // Навигационные свойства
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
        public virtual ICollection<AuthorImage> AuthorImages { get; set; } = new List<AuthorImage>();


        // Вычисляемое свойство (не сохраняется в БД)
        [NotMapped]
        public string FullName { get; private set; } = string.Empty;

        protected Author() { }

        public Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}