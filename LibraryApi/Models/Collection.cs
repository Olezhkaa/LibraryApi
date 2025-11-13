using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Models
{
    [Table("collection")]
    public class Collection : BaseModel
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        public virtual ICollection<CollectionBook> CollectionBooks { get; set; } = new List<CollectionBook>();

        protected Collection() { }

        public Collection(string title)
        {
            Title = title;
        }
    }
}