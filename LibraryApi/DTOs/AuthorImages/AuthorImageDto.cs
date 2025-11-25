namespace LibraryApi.DTOs.AuthorImages
{
    public class AuthorImageDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = Array.Empty<byte>();
        public long FileSize { get; set; }
        public string Url { get; set; } = string.Empty;

        public bool IsMain { get; set; } = false;

    }
}