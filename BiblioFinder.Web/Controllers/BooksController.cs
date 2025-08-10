using AutoMapper;
using BiblioFinder.Application.Contracts.Services;
using BiblioFinder.Application.UseCases;
using BiblioFinder.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BiblioFinder.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly SearchBooksUseCase _searchBooksUseCase;
        private readonly IMapper _mapper;

        public BooksController(BookService bookService, IMapper mapper, SearchBooksUseCase searchBooksUseCase)
        {
            _searchBooksUseCase = searchBooksUseCase;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(new List<BookViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string? author)
        {
            author = author?.Trim();

            ViewData["SearchedAuthor"] = author;

            if (string.IsNullOrWhiteSpace(author))
            {
                return View("Index", new List<BookViewModel>());
            }

            var booksDto = await _searchBooksUseCase.ExecuteAsync(author);

            var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(booksDto);

            return View("Index", bookViewModels);
        }
    }
}
