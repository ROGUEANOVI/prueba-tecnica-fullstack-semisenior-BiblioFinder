namespace BiblioFinder.Application.Dtos
{
    public class SearchHistoryDto
    {
        public int Id { get; set; }
        public required string Author { get; set; }
        public required string Title { get; set; }
        public int? PublicationYear { get; set; }
        public string? Publisher { get; set; }
        public DateTime QueryDate { get; set; }
    }
}
