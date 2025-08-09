using BiblioFinder.Application.Dtos;
using BiblioFinder.Application.Services;
using System.Net.Http.Json;

namespace BiblioFinder.Infrastructure.Services
{
    public class OpenLibraryBookService : BookService
    {
        private readonly HttpClient _httpClient;

        public OpenLibraryBookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("https://openlibrary.org/");
        }

        public async Task<IEnumerable<BookDto>> SearchByAuthorAsync(string authorName)
        {
            var response = await _httpClient.GetAsync($"search.json?author={authorName}");
            response.EnsureSuccessStatusCode();

            var searchResponse = await response.Content.ReadFromJsonAsync<OpenLibrarySearchResponseDto>();

            return searchResponse?.Docs ?? Enumerable.Empty<BookDto>();
        }
    }
}
