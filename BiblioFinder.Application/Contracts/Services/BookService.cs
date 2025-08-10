using BiblioFinder.Application.Dtos;

namespace BiblioFinder.Application.Contracts.Services
{
    public interface BookService
    {
        Task<IEnumerable<BookDto>> SearchByAuthorAsync(string authorName);
    }
}
