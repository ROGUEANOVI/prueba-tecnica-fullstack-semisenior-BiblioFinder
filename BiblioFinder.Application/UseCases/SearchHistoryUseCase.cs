using AutoMapper;
using BiblioFinder.Application.Contracts.Repositories;
using BiblioFinder.Application.Dtos;

namespace BiblioFinder.Application.UseCases
{
    public class SearchHistoryUseCase
    {
        private readonly SearchHistoryRepository _searchHistoryRepository;
        private readonly IMapper _mapper;

        public SearchHistoryUseCase(SearchHistoryRepository searchHistoryRepository, IMapper mapper)
        {
            _searchHistoryRepository = searchHistoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SearchHistoryDto>> GetAllAsync() {
            var historyRecords = await _searchHistoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SearchHistoryDto>>(historyRecords);
        }
    }
}
