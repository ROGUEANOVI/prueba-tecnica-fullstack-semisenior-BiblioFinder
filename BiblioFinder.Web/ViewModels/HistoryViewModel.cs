namespace BiblioFinder.Web.ViewModels
{
    public class HistoryViewModel
    {
        public required string Id { get; set; }
        public required string Author { get; set; }
        public required string Title { get; set; }
        public required string PublicationYear { get; set; } 
        public required string Publisher { get; set; }
        public required string QueryDate { get; set; }
    }
}
