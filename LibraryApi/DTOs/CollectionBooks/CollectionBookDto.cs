namespace LibraryApi.DTOs.CollectionBook
{
    public class CollectionBookDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CollectionId { get; set; }
        public int BookId { get; set; }

        // Связанные данные
        public string UserFullName { get; set; } = string.Empty;
        public string CollectionTitle { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string BookAuthor { get; set; } = string.Empty;
    }
}