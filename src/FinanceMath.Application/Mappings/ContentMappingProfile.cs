using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;

namespace FinanceMath.Application.Mappings
{
    public class ContentMappingProfile : Profile
    {
        public ContentMappingProfile()
        {
            CreateMap<Domain.ContentAggregate.Content, ContentDto>().ReverseMap();
        }
    }
}
