using System.Text.Json.Serialization;

namespace BiblioFinder.Application.Dtos
{
    public class OpenLibrarySearchResponseDto
    {
        [JsonPropertyName("docs")]
        public List<BookDto> Docs { get; set; } = [];
    }
}
