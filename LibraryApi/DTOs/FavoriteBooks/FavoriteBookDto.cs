namespace LibraryApi.DTOs.FavoriteBooks
{
    public class FavoriteBookDto
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public int PriorityInList { get; set; }
    }
}