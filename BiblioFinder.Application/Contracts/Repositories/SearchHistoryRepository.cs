using BiblioFinder.Domain.Entities;

namespace BiblioFinder.Application.Contracts.Repositories
{
    public interface SearchHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> GetAllAsync();
     
        Task<SearchHistory?> GetLastByAuthorAsync(string authorName);

        Task AddRangeAsync(IEnumerable<SearchHistory> searchHistories);
    }
}
