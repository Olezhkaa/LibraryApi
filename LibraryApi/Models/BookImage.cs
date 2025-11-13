using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    [Table("book_images")]
    public class BookImage : BaseModel
    {
        [Required]
        [Column("book_id")]
        public int BookId { get; set; }

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
        public virtual Book? Book { get; set; }

        protected BookImage() { }

        public BookImage(int bookId, string fileName, string contentType, byte[] fileData, bool isMain = false)
        {
            BookId = bookId;
            FileName = fileName;
            ContentType = contentType;
            FileData = fileData;
            FileSize = fileData.Length;
            IsMain = isMain;
        }
    }
}