using AutoMapper;
using BiblioFinder.Application.Dtos;
using BiblioFinder.Web.ViewModels;
using System.Linq;

namespace BiblioFinder.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, BookViewModel>()
                .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear.HasValue ? src.PublicationYear.Value.ToString() : "N/A"))
                .ForMember(dest => dest.Publishers, opt => opt.MapFrom(src => src.Publishers != null && src.Publishers.Any() ? string.Join(", ", src.Publishers) : "N/A"));
        }
    }
}
