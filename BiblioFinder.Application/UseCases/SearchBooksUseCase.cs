using BiblioFinder.Application.Contracts.Repositories;
using BiblioFinder.Application.Contracts.Services;
using BiblioFinder.Application.Dtos;
using BiblioFinder.Domain.Entities;

namespace BiblioFinder.Application.UseCases
{
    public class SearchBooksUseCase
    {
        private readonly BookService _bookApiService;
        private readonly SearchHistoryRepository _searchHistoryRepository;

        public SearchBooksUseCase(BookService bookApiService, SearchHistoryRepository searchHistoryRepository)
        {
            _bookApiService = bookApiService;
            _searchHistoryRepository = searchHistoryRepository;
        }

        public async Task<IEnumerable<BookDto>> ExecuteAsync(string authorName)
        {
            var lastSearch = await _searchHistoryRepository.GetLastByAuthorAsync(authorName);
            bool isRecentSearch = lastSearch != null && (DateTime.UtcNow - lastSearch.QueryDate).TotalMinutes < 1;

            var booksDto = await _bookApiService.SearchByAuthorAsync(authorName);

            if (booksDto.Any() && !isRecentSearch)
            {
                var historyToSave = booksDto.Select(book => new SearchHistory
                {
                    Author = authorName,
                    Title = book.Title,
                    PublicationYear = book.PublicationYear,
                    Publisher = book.Publisher.Where(p => string.Join(",", p).Length > 0).FirstOrDefault(),
                    QueryDate = DateTime.UtcNow
                });

                await _searchHistoryRepository.AddRangeAsync(historyToSave);
            }

            return booksDto;
        }
    }
}
