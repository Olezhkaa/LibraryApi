using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryApi.Models
{
    [Table("user_images")]
    public class UserImage : BaseModel
    {
        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

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
        public virtual User? User { get; set; }

        protected UserImage() { }

        public UserImage(int userId, string fileName, string contentType, byte[] fileData, bool isMain = false)
        {
            UserId = userId;
            FileName = fileName;
            ContentType = contentType;
            FileData = fileData;
            FileSize = fileData.Length;
            IsMain = isMain;
        }
    }
}