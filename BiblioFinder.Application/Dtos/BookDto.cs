using System.Text.Json.Serialization;

namespace BiblioFinder.Application.Dtos
{
    public class BookDto
    {
        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("first_publish_year")]
        public int? PublicationYear { get; set; }

        [JsonPropertyName("publisher")]
        public List<string>? Publishers { get; set; }
    }
}