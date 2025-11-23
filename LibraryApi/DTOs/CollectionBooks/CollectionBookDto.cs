namespace LibraryApi.DTOs.CollectionBook
{
    public class CollectionBookDto
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string CollectionTitle { get; set; } = string.Empty;
    }
}