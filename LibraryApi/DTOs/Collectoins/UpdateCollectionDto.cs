using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.DTOs.Collectoins
{
    public class UpdateCollectionDto
    {
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Название должно быть не менее 1 и не более 200 символов")]
        public string Title { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}