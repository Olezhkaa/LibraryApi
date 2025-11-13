using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    [Table("users")]
    public class User : BaseModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [JsonIgnore]
        [Column("password_hash")]
        public string PasswordHash { get; set; } = string.Empty;

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

        public virtual ICollection<CollectionBook> CollectionBooks { get; set; } = new List<CollectionBook>();
        public virtual ICollection<FavoriteBook> FavoriteBooks { get; set; } = new List<FavoriteBook>();
        public virtual ICollection<UserImage> UserImages { get; set; } = new List<UserImage>();


        protected User() { }

        public User(string email, string passwordHash, string firstName, string lastName)
        {
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
        }


    }
}