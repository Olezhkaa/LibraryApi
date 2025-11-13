using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Models
{
    [Table("authors")]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 2)]
        [Column("middle_name")]
        public string MiddleName { get; set; } = string.Empty;

        [Column("date_of_birth")]
        public DateTime? DateOfBirh { get; set; }

        [Column("date_of_death")]
        public DateTime? DateOfDeath { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Column("country")]
        public string Country { get; set; } = string.Empty;

        // [Column("created_at")]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        // Вычисляемое свойство (не сохраняется в БД)
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        protected Author() { }

        public Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}