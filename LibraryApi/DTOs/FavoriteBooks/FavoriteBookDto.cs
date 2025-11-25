namespace LibraryApi.DTOs.FavoriteBooks
{
    public class FavoriteBookDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int PriorityInList { get; set; }

        // Связанные данные книги
        public string BookTitle { get; set; } = string.Empty;
        public string BookAuthor { get; set; } = string.Empty;
    }
}