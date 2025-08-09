using AutoMapper;
using BiblioFinder.Application.Services;
using BiblioFinder.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BiblioFinder.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(BookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(new List<BookViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> Search(string author)
        {    
            ViewData["SearchedAuthor"] = author;
            
            if (string.IsNullOrWhiteSpace(author))
            {
                return View("Index", new List<BookViewModel>());
            }

            var booksDto = await _bookService.SearchByAuthorAsync(author);
            var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(booksDto);

            return View("Index", bookViewModels);
        }
    }
}
