using AutoMapper;
using BiblioFinder.Application.Dtos;
using BiblioFinder.Domain.Entities;
using BiblioFinder.Web.ViewModels;

namespace BiblioFinder.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, BookViewModel>()
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear.HasValue ? src.PublicationYear.Value.ToString() : "N/A"))
                .ForMember(dest => dest.Publishers, opt => opt.MapFrom(src => src.Publisher != null && src.Publisher.Any() ? string.Join(", ", src.Publisher) : "N/A"));

            CreateMap<SearchHistory, SearchHistoryDto>();
            CreateMap<SearchHistoryDto, SearchHistory>();

            CreateMap<SearchHistoryDto, HistoryViewModel>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear.HasValue ? src.PublicationYear.Value.ToString() : "N/A"))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher ?? "N/A"))
                .ForMember(dest => dest.QueryDate, opt => opt.MapFrom(src => src.QueryDate.ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss")));
        }
    }
}
