using BiblioFinder.Application.Dtos;

namespace BiblioFinder.Application.Services
{
    public interface BookService
    {
        Task<IEnumerable<BookDto>> SearchByAuthorAsync(string authorName);
    }
}
