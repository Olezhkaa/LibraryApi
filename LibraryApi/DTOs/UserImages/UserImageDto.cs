namespace LibraryApi.DTOs.UserImages
{
    public class UserImageDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = Array.Empty<byte>();
        public long FileSize { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}