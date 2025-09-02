using AutoMapper;
using FinanceMath.Application.Contents.Dtos;
using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Application.Mappings
{
    public class ContentMappingProfile : Profile
    {
        public ContentMappingProfile()
        {
            CreateMap<Content, ContentDto>();
            CreateMap<ContentDto, Content>();
        }
    }
}
