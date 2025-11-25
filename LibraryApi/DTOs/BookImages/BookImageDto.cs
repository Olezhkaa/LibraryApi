namespace LibraryApi.DTOs.BookImages
{
    public class BookImageDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = Array.Empty<byte>();
        public long FileSize { get; set; }
        public string Url { get; set; } = string.Empty;

        public bool IsMain { get; set; } = false;
    }
}