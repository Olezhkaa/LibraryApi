using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    [Table("author_images")]
    public class AuthorImage : BaseModel
    {
        [Required]
        [Column("author_id")]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("file_name")]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("content_type")]
        public string ContentType { get; set; } = string.Empty;

        [Required]
        [Column("file_data")]
        public byte[] FileData { get; set; } = Array.Empty<byte>(); //Бинарные данные

        [Required]
        [Column("file_size")]
        public long FileSize { get; set; }

        [Required]
        [Column("is_main")]
        public bool IsMain { get; set; } = false;

        [JsonIgnore]
        public virtual Author? Author { get; set; }

        protected AuthorImage() { }

        public AuthorImage(int authorId, string fileName, string contentType, byte[] fileData, bool isMain = false)
        {
            AuthorId = authorId;
            FileName = fileName;
            ContentType = contentType;
            FileData = fileData;
            FileSize = fileData.Length;
            IsMain = isMain;
        }
    }
}