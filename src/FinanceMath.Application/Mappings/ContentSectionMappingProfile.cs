using AutoMapper;
using FinanceMath.Application.Content.ContentSections.Dtos;
using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Application.Mappings
{
    public class ContentSectionMappingProfile : Profile
    {
        public ContentSectionMappingProfile()
        {
            CreateMap<ContentSection, ContentSectionDto>()
                .ForMember(dest => dest.ContentId, opt => opt.MapFrom(src => src.Content.Id))
                .ReverseMap();
        }
    }
}
