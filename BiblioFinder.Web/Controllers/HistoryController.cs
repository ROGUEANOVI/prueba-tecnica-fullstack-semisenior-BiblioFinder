using AutoMapper;
using BiblioFinder.Application.UseCases;
using BiblioFinder.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BiblioFinder.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly SearchHistoryUseCase _searchHistoryUseCase;
        private readonly IMapper _mapper;

        public HistoryController(SearchHistoryUseCase searchHistoryUseCase, IMapper mapper)
        {
            _searchHistoryUseCase = searchHistoryUseCase;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var historyRecords = await _searchHistoryUseCase.GetAllAsync();

            var historyViewModels = _mapper.Map<IEnumerable<HistoryViewModel>>(historyRecords);

            return View(historyViewModels);
        }
    }
}
